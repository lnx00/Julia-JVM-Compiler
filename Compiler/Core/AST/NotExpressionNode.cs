using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class NotExpressionNode : ExpressionNode
{
    public override TypeManager.DataType Type { get; } = TypeManager.DataType.Bool;
    public ExpressionNode Expression { get; }
    
    public NotExpressionNode(ExpressionNode expression)
    {
        Expression = expression;
    }
}