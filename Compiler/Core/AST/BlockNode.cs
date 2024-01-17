using Compiler.CodeGenerator;

namespace Compiler.Core.AST;

public class BlockNode : INode
{
    public List<INode> Statements { get; }

    public BlockNode(List<INode> statements)
    {
        Statements = statements;
    }

    public override List<string> Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        foreach (var statement in Statements)
        {
            instructions.AddRange(statement.Translate(ctx));
        }
        
        return instructions;
    }
}