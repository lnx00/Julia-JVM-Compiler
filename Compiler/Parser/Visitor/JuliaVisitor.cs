using Compiler.Core.AST;
using Compiler.Core.Common;
using Compiler.Core.SymbolTable;
using Compiler.Parser.ErrorHandling;

namespace Compiler.Parser.Visitor;

public class JuliaVisitor : JuliaBaseVisitor<INode?>
{
    private readonly SymbolTable _symbolTable = new();
    
    public override INode? VisitDeclaration(JuliaParser.DeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var typeName = context.type().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();

        // Check if variable already exists
        if (_symbolTable.IsDefined(varName))
        {
            throw new Exception($"Variable {varName} already defined");
        }
        
        // Check for type compatibility
        var varType = TypeManager.GetDataType(typeName) ?? throw SyntaxErrorException.Create($"Unknown variable type {typeName}", context);
        if (varType != value.Type)
        {
            throw TypeMismatchException.Create(varType, value.Type, context);
        }

        _symbolTable.AddSymbol(varName, varType);

        return null;
    }

    public override INode? VisitAssignment(JuliaParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();
        
        // Check for type compatibility
        var varType = _symbolTable.GetSymbol(varName) ?? throw UndefinedVarException.Create(varName, context);
        if (varType != value.Type)
        {
            throw TypeMismatchException.Create(varType, value.Type, context);
        }
        
        return null;
    }

    public override INode? VisitConst(JuliaParser.ConstContext context)
    {
        if (context.INTCONST() != null)
        {
            int value = int.Parse(context.INTCONST().GetText());
            return new IntegerConstNode(value);
        }
        
        if (context.FLTCONST() != null)
        {
            float value = float.Parse(context.FLTCONST().GetText());
            return new FloatConstNode(value);
        }
        
        if (context.STRCONST() != null)
        {
            string value = context.STRCONST().GetText();
            return new StringConstNode(value);
        }
        
        if (context.BOOLCONST() != null)
        {
            bool value = bool.Parse(context.BOOLCONST().GetText());
            return new BoolConstNode(value);
        }
        
        throw new Exception("Unknown constant type");
    }

    public override INode? VisitParenExpr(JuliaParser.ParenExprContext context)
    {
        return Visit(context.expression());
    }

    public override INode? VisitNotExpr(JuliaParser.NotExprContext context)
    {
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();
        if (value.Type != TypeManager.DataType.Bool)
        {
            throw InvalidOperatorException.Create("!", context);
        }
        
        return new NotExpressionNode(value);
    }

    public override INode? VisitVarExpr(JuliaParser.VarExprContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        
        // Check if variable exists
        var varType = _symbolTable.GetSymbol(varName) ?? throw UndefinedVarException.Create(varName, context);
        
        return new IdentifierNode(varName, varType);
    }

    public override INode? VisitAddExpr(JuliaParser.AddExprContext context)
    {
        var op = context.addOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();

        var type = TypeManager.GetCommonNumericType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);
        
        return op switch
        {
            "+" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Add, type),
            "-" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Subtract, type),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode? VisitMultExpr(JuliaParser.MultExprContext context)
    {
        var op = context.multOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();
        
        var type = TypeManager.GetCommonNumericType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);

        return op switch
        {
            "*" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mult, type),
            "/" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Div, type),
            "%" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mod, type),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode? VisitBoolExpr(JuliaParser.BoolExprContext context)
    {
        var op = context.boolOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();

        // Are both sides bools?
        if (left.Type != TypeManager.DataType.Bool || right.Type != TypeManager.DataType.Bool)
        {
            throw TypeMismatchException.Create(left.Type, right.Type, context);
        }
        
        return op switch
        {
            "&&" => new BoolExpressionNode(left, right, BoolExpressionNode.Operation.And, TypeManager.DataType.Bool),
            "||" => new BoolExpressionNode(left, right, BoolExpressionNode.Operation.Or, TypeManager.DataType.Bool),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode? VisitCompExpr(JuliaParser.CompExprContext context)
    {
        var op = context.compOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();
        
        var type = TypeManager.GetCommonType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);
        
        // String and bool comparison
        if (type is TypeManager.DataType.String or TypeManager.DataType.Bool)
        {
            return op switch
            {
                "==" => new CompExpressionNode(left, right, CompExpressionNode.Operation.Equal),
                "!=" => new CompExpressionNode(left, right, CompExpressionNode.Operation.NotEqual),
                _ => throw InvalidOperatorException.Create(op, context)
            };
        }

        // Numeric comparison
        return op switch
        {
            "==" => new CompExpressionNode(left, right, CompExpressionNode.Operation.Equal),
            "!=" => new CompExpressionNode(left, right, CompExpressionNode.Operation.NotEqual),
            "<" => new CompExpressionNode(left, right, CompExpressionNode.Operation.LessThan),
            "<=" => new CompExpressionNode(left, right, CompExpressionNode.Operation.LessThanOrEqual),
            ">" => new CompExpressionNode(left, right, CompExpressionNode.Operation.GreaterThan),
            ">=" => new CompExpressionNode(left, right, CompExpressionNode.Operation.GreaterThanOrEqual),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode? VisitFunction(JuliaParser.FunctionContext context)
    {
        _symbolTable.EnterScope();
        
        var funcName = context.IDENTIFIER().GetText();
        var funcParams = Visit(context.parameters());
        
        // TODO: Add function to symbol table
        
        // Hacky, but it works for now
        if (context.type() != null)
        {
            var returnTypeName = context.type().GetText();
            var returnType = TypeManager.GetDataType(returnTypeName)
                             ?? throw SyntaxErrorException.Create($"Unknown return type {returnTypeName}", context);
            _symbolTable.AddSymbol("#return", returnType);
        }
        
        var funcBody = Visit(context.body());

        _symbolTable.LeaveScope();
        return null;
    }

    public override INode? VisitParameters(JuliaParser.ParametersContext context)
    {
        for (int i = 0; i < context.IDENTIFIER().Length; i++)
        {
            var varName = context.IDENTIFIER(i).GetText();
            var typeName = context.type(i).GetText();
            
            // Check if variable already exists
            if (_symbolTable.IsDefined(varName))
            {
                throw new Exception($"Variable {varName} already defined");
            }
            
            // Add to symbol table
            var varType = TypeManager.GetDataType(typeName) ?? throw SyntaxErrorException.Create($"Unknown parameter type {typeName}", context);
            _symbolTable.AddSymbol(varName, varType);
        }

        return null;
    }

    public override INode? VisitReturn(JuliaParser.ReturnContext context)
    {
        // Is there a return expression?
        if (context.expression() == null) return null;
        
        // Evaluate the return expression
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();
        var returnType = _symbolTable.GetSymbol("#return")
                         ?? throw SyntaxErrorException.Create("Function has not return type", context);
            
        // Check for type compatibility
        if (returnType != value.Type)
        {
            throw TypeMismatchException.Create(returnType, value.Type, context);
        }

        return value;
    }

    public override INode? VisitBody(JuliaParser.BodyContext context)
    {
        _symbolTable.EnterScope();
        
        foreach (var statementContex in context.statement())
        {
            Visit(statementContex);
        }

        _symbolTable.LeaveScope();
        return null;
    }
}