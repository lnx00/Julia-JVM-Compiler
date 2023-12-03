using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class MultExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Mult,
        Div,
        Mod
    }
    
    public override TypeManager.DataType Type { get; }
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public MultExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op, TypeManager.DataType type)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        Type = type;
        OperationType = op;
    }
}