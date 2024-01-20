using Compiler.CodeGenerator;

namespace Compiler.Core.AST;

public class BlockNode : INode
{
    public List<INode> Statements { get; }

    public BlockNode(List<INode> statements)
    {
        Statements = statements;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        int stackSize = 0;
        
        foreach (var result in Statements.Select(statement => statement.Translate(ctx)))
        {
            instructions.AddRange(result.Instructions);
            stackSize = Math.Max(stackSize, result.StackSize);
        }
        
        return new TranslationResult(instructions, stackSize);
    }
}