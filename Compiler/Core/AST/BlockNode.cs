namespace Compiler.Core.AST;

public class BlockNode : INode
{
    public List<INode> Statements { get; }

    public BlockNode(List<INode> statements)
    {
        Statements = statements;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new();
        foreach (var statement in Statements)
        {
            instructions.AddRange(statement.Translate());
        }
        
        return instructions;
    }
}