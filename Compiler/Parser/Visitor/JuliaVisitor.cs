using Compiler.Core.AST;
using Compiler.Core.Common;
using Compiler.Core.SymbolTable;
using Compiler.Parser.ErrorHandling;

namespace Compiler.Parser.Visitor;

public class JuliaVisitor : JuliaBaseVisitor<INode?>
{
    private readonly SymbolTable _symbolTable = new();
    private bool _isPeeking = true;

    public override INode VisitStart(JuliaParser.StartContext context)
    {
        /* HACK: Hotfix for late function declarations */
        
        // Peek all functions | TODO: Do this in a separate visitor
        foreach (var function in context.function())
        {
            Visit(function);
        }

        _isPeeking = false;
        List<INode> statements = new();
        
        // Visit all statements
        statements.AddRange(context.statement().Select(expression => Visit(expression) ?? throw new NotImplementedException()));
        
        // Visit all functions again
        statements.AddRange(context.function().Select(function => Visit(function) ?? throw new NotImplementedException()));

        return new BlockNode(statements);
    }

    public override INode VisitDeclaration(JuliaParser.DeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var typeName = context.type().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);

        // Check if variable already exists
        if (_symbolTable.IsDefined(varName))
        {
            throw new Exception($"Variable {varName} already defined");
        }
        
        // Check for type compatibility
        var varType = TypeManager.GetDataType(typeName) ?? throw SyntaxErrorException.Create($"Unknown variable type {typeName}", context);
        if (varType != value.Type)
        {
            // Check for implicit conversion
            if (value is IntegerConstNode node && varType == TypeManager.DataType.Float64)
            {
                value = new FloatConstNode(node.Value);
            }
            else
            {
                throw TypeMismatchException.Create(varType, value.Type, context);
            }
        }

        _symbolTable.AddVariable(varName, varType);

        return new DeclarationNode(varName, varType, value);
    }

    public override INode VisitAssignment(JuliaParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        
        // Check for type compatibility
        var varSymbol = _symbolTable.GetVariable(varName) ?? throw UndefinedVarException.Create(varName, context);
        if (varSymbol.Type != value.Type)
        {
            throw TypeMismatchException.Create(varSymbol.Type, value.Type, context);
        }
        
        return new AssignmentNode(varName, value);
    }

    public override INode VisitConst(JuliaParser.ConstContext context)
    {
        if (context.intValue() != null)
        {
            int value = int.Parse(context.intValue().GetText());
            return new IntegerConstNode(value);
        }
        
        if (context.floatValue() != null)
        {
            double value = double.Parse(context.floatValue().GetText());
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

    public override INode VisitParenExpr(JuliaParser.ParenExprContext context)
    {
        return Visit(context.expression()) ?? throw new NotImplementedException();
    }

    public override INode VisitUnaryExpr(JuliaParser.UnaryExprContext context)
    {
        var value = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        var op = context.unaryOp().GetText();

        // Numeric operations
        if (TypeManager.IsNumeric(value.Type))
        {
            return op switch
            {
                "-" => new UnaryExpressionNode(value, UnaryExpressionNode.Operation.Negate),
                "+" => value,
                _ => throw InvalidOperatorException.Create(op, context)
            };
        }
        
        // Boolean operations
        if (TypeManager.IsBoolean(value.Type))
        {
            return op switch
            {
                "!" => new UnaryExpressionNode(value, UnaryExpressionNode.Operation.Not),
                _ => throw InvalidOperatorException.Create(op, context)
            };
        }
        
        throw InvalidOperatorException.Create(op, context);
    }

    public override INode VisitVarExpr(JuliaParser.VarExprContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        
        // Check if variable exists
        var varSymbol = _symbolTable.GetVariable(varName) ?? throw UndefinedVarException.Create(varName, context);
        
        return new IdentifierNode(varName, varSymbol.Type);
    }

    public override INode VisitAddExpr(JuliaParser.AddExprContext context)
    {
        var op = context.addOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);

        var type = TypeManager.GetCommonNumericType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);
        
        return op switch
        {
            "+" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Add, type),
            "-" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Subtract, type),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode VisitMultExpr(JuliaParser.MultExprContext context)
    {
        var op = context.multOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        
        var type = TypeManager.GetCommonNumericType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);

        return op switch
        {
            "*" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mult, type),
            "/" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Div, type),
            "%" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mod, type),
            _ => throw InvalidOperatorException.Create(op, context)
        };
    }

    public override INode VisitBoolExpr(JuliaParser.BoolExprContext context)
    {
        var op = context.boolOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);

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

    public override INode VisitCompExpr(JuliaParser.CompExprContext context)
    {
        var op = context.compOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        
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

    public override INode VisitFunction(JuliaParser.FunctionContext context)
    {
        // Create the function symbol
        var funcName = context.IDENTIFIER().GetText();
        var returnType = TypeManager.DataType.Void;
        if (context.type() != null)
        {
            // Retrieve the return type
            var returnTypeName = context.type().GetText();
            returnType = TypeManager.GetDataType(returnTypeName)
                         ?? throw SyntaxErrorException.Create($"Unknown return type {returnTypeName}", context);
            
            var funcSymbol = _isPeeking ? _symbolTable.AddFunction(funcName, returnType) : _symbolTable.GetFunction(funcName);
            _symbolTable.EnterFunctionScope(funcSymbol ?? throw SyntaxErrorException.Create(context));
        }
        else
        {
            // No return type
            var funcSymbol = _isPeeking ? _symbolTable.AddFunction(funcName, TypeManager.DataType.Void) : _symbolTable.GetFunction(funcName);
            _symbolTable.EnterFunctionScope(funcSymbol ?? throw SyntaxErrorException.Create(context));
        }
        
        /* FUNCTION SCOPE BEGIN */
        
        // HACK: Hotfix for parameter type checking
        if (_isPeeking)
        {
            Visit(context.parameters());
            
            // TODO - HACK
            _symbolTable.LeaveFunctionScope();
            return new FunctionDefinitionNode("", TypeManager.DataType.Any, new BlockNode(new List<INode>()), new List<ParameterNode>());
        }
        else
        {
            var funcParams = Visit(context.parameters());
            var funcBody = Visit(context.body()) as BlockNode ?? throw SyntaxErrorException.Create(context);
            
            // TODO: This is not correct!!!
            _symbolTable.LeaveFunctionScope();
            return new FunctionDefinitionNode(funcName, returnType, funcBody, new List<ParameterNode>());
        }
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
            var paramSymbol = _symbolTable.AddVariable(varName, varType);

            if (_isPeeking)
            {
                _symbolTable.GetCurrentFunction()?.Parameters.Add(paramSymbol);
            }
        }

        return null;
    }

    public override INode VisitReturn(JuliaParser.ReturnContext context)
    {
        var funcSymbol = _symbolTable.GetCurrentFunction() ?? throw SyntaxErrorException.Create("Return outside of function", context);
        var returnType = funcSymbol.Type;
        
        // Is there no return expression?
        if (context.expression() == null)
        {
            // Is it a void function?
            if (returnType != TypeManager.DataType.Void)
            {
                throw TypeMismatchException.Create(returnType, TypeManager.DataType.Void, context);
            }
            
            return new ReturnNode(null);
        }

        // Evaluate the return expression
        var value = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
            
        // Check for type compatibility
        if (returnType != value.Type)
        {
            throw TypeMismatchException.Create(returnType, value.Type, context);
        }

        return new ReturnNode(value);
    }

    public override INode VisitBlock(JuliaParser.BlockContext context)
    {
        return Visit(context.body()) ?? throw new NotImplementedException();
    }

    public override INode VisitBody(JuliaParser.BodyContext context)
    {
        _symbolTable.EnterScope();
        
        List<INode> statements = context.statement().Select(Visit).OfType<INode>().ToList();

        _symbolTable.LeaveScope();
        return new BlockNode(statements);
    }

    public override INode VisitCall(JuliaParser.CallContext context)
    {
        var funcName = context.IDENTIFIER().GetText();
        var funcSymbol = _symbolTable.GetFunction(funcName) ?? throw UndefinedFuncException.Create(funcName, context);

        // Handle function parameters
        List<ExpressionNode> arguments = context.expression()
            .Select(expressionContext => Visit(expressionContext) as ExpressionNode ?? throw new InvalidOperationException())
            .ToList();
        
        // Check if number of arguments matches
        if (funcSymbol.Parameters.Count != arguments.Count)
        {
            throw SyntaxErrorException.Create("Parameter count mismatch in function call", context);
        }
        
        // Check if argument types match
        for (int i = 0; i < arguments.Count; i++)
        {
            var argType = arguments[i].Type;
            var paramType = funcSymbol.Parameters[i].Type;
            if (argType != paramType && paramType != TypeManager.DataType.Any)
            {
                throw TypeMismatchException.Create(paramType, argType, context);
            }
        }

        // Check if function has a return type
        return new CallNode(funcName, arguments, funcSymbol.Type);
    }

    public override INode VisitIf(JuliaParser.IfContext context)
    {
        var condition = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        if (condition.Type != TypeManager.DataType.Bool)
        {
            throw TypeMismatchException.Create(TypeManager.DataType.Bool, condition.Type, context);
        }
        
        // If branch
        var body = Visit(context.body(0)) as BlockNode ?? throw SyntaxErrorException.Create(context);
        
        // Else branch
        if (context.body().Length > 1)
        {
            var elseBody = Visit(context.body(1)) as BlockNode ?? throw SyntaxErrorException.Create(context);
            return new IfNode(condition, body, elseBody);
        }
        
        return new IfNode(condition, body, null);
    }

    public override INode VisitWhile(JuliaParser.WhileContext context)
    {
        var condition = Visit(context.expression()) as ExpressionNode ?? throw SyntaxErrorException.Create(context);
        if (condition.Type != TypeManager.DataType.Bool)
        {
            throw TypeMismatchException.Create(TypeManager.DataType.Bool, condition.Type, context);
        }

        var body = Visit(context.body()) as BlockNode ?? throw SyntaxErrorException.Create(context);
        
        return new WhileNode(condition, body);
    }
}