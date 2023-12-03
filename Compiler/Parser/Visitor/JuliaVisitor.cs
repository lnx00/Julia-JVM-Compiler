using Compiler.Core.AST;
using Compiler.Core.Common;
using Compiler.Parser.ErrorHandling;

namespace Compiler.Parser.Visitor;

public class JuliaVisitor : JuliaBaseVisitor<INode?>
{
    private readonly Dictionary<string, TypeManager.DataType> _variables = new();
    
    public override INode? VisitDeclaration(JuliaParser.DeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var typeName = context.type().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();
        
        // Check for type compatibility
        var type = TypeManager.GetDataType(typeName);
        if (type != value.Type)
        {
            throw new Exception("Cannot initialize variable of type " + typeName + " with value of type " + value.Type);
        }

        _variables.Add(varName, type);

        //return base.VisitDeclaration(context);
        return null;
    }

    public override INode? VisitAssignment(JuliaParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression()) as ExpressionNode ?? throw new InvalidOperationException();
        
        // Check for type compatibility
        var type = _variables[varName];
        if (type != value.Type)
        {
            throw new Exception("Cannot assign variable of type " + type + " with value of type " + value.Type);
        }
        
        //return base.VisitAssignment(context);
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
        
        //return base.VisitConst(context);
        throw new Exception("Unknown constant type");
    }

    public override INode? VisitParenExpr(JuliaParser.ParenExprContext context)
    {
        return Visit(context.expression());
        //return base.VisitParenExpr(context);
    }

    public override INode? VisitVarExpr(JuliaParser.VarExprContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        
        // Check if variable exists
        if (!_variables.ContainsKey(varName))
        {
            throw new Exception("Unknown variable: " + varName);
        }
        
        return new IdentifierNode(varName, _variables[varName]);
        //return base.VisitVarExpr(context);
    }

    public override INode? VisitAddExpr(JuliaParser.AddExprContext context)
    {
        var op = context.addOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();

        var type = TypeManager.GetCommonType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);
        
        return op switch
        {
            "+" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Add, type),
            "-" => new AddExpressionNode(left, right, AddExpressionNode.Operation.Subtract, type),
            _ => throw new Exception("Unknown operator: " + op)
        };
    }

    public override INode? VisitMultExpr(JuliaParser.MultExprContext context)
    {
        var op = context.multOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();
        
        var type = TypeManager.GetCommonType(left.Type, right.Type) ?? throw TypeMismatchException.Create(left.Type, right.Type, context);

        return op switch
        {
            "*" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mult, type),
            "/" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Div, type),
            "%" => new MultExpressionNode(left, right, MultExpressionNode.Operation.Mod, type),
            _ => throw new Exception("Unknown operator: " + op)
        };
    }

    public override INode? VisitBoolExpr(JuliaParser.BoolExprContext context)
    {
        var op = context.boolOp().GetText();
        var left = Visit(context.expression(0)) as ExpressionNode ?? throw new InvalidOperationException();
        var right = Visit(context.expression(1)) as ExpressionNode ?? throw new InvalidOperationException();

        if (left.Type == TypeManager.DataType.Bool && right.Type == TypeManager.DataType.Bool)
        {
            return new BoolExpressionNode();
        }
        
        throw TypeMismatchException.Create(left.Type, right.Type, context);
        //return base.VisitBoolExpr(context);
    }
}