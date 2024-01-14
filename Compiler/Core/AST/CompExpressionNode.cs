using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class CompExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    public override TypeManager.DataType Type { get; } = TypeManager.DataType.Bool;
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public CompExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        OperationType = op;
    }

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}