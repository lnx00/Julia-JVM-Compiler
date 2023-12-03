using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class AddExpressionNode : ExpressionNode
{
    public override TypeManager.DataType Type { get; }
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }

    public AddExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        Type = TypeManager.GetAddExpressionType(leftExpression.Type, rightExpression.Type);
    }
}