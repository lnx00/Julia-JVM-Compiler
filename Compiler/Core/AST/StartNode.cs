using Compiler.CodeGenerator;

namespace Compiler.Core.AST;

public class StartNode : INode
{
    public List<INode> Statements { get; }

    public StartNode(List<INode> statements)
    {
        Statements = statements;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        int stackSize = 0;

        foreach (var stmt in Statements)
        {
            if (stmt is not FunctionDefinitionNode funcDef) { continue; }
            
            var result = funcDef.Translate(ctx);
            instructions.AddRange(result.Instructions);
            stackSize = Math.Max(stackSize, result.StackSize);
        }
        
        return new TranslationResult(instructions, stackSize);
    }
}