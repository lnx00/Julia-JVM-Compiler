namespace Compiler.Core.AST;

public class WhileNode : INode
{
    public ExpressionNode Condition { get; }
    public BlockNode Body { get; }

    public WhileNode(ExpressionNode condition, BlockNode body)
    {
        Condition = condition;
        Body = body;
    }

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}