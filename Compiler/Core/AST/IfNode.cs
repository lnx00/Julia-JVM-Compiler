namespace Compiler.Core.AST;

public class IfNode : INode
{
    public ExpressionNode Condition { get; }
    public BlockNode Body { get; }
    public BlockNode? ElseBody { get; }

    public IfNode(ExpressionNode condition, BlockNode body, BlockNode? elseBody)
    {
        Condition = condition;
        Body = body;
        ElseBody = elseBody;
    }
}