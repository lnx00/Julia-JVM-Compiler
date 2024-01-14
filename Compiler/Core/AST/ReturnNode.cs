namespace Compiler.Core.AST;

public class ReturnNode : INode
{
    public ExpressionNode? Value { get; }

    public ReturnNode(ExpressionNode? value)
    {
        Value = value;
    }

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}