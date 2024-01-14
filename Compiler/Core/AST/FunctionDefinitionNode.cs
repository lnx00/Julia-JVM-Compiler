using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class FunctionDefinitionNode : INode
{
    public FunctionSymbol Symbol { get; }
    public BlockNode Block { get; }
    public ParameterNode Parameters { get; }

    public FunctionDefinitionNode(FunctionSymbol symbol, BlockNode block, ParameterNode parameters)
    {
        Symbol = symbol;
        Block = block;
        Parameters = parameters;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new();
        
        // Method header
        // Special case for main | TODO: Don't hardcode this
        if (Symbol.Name == "main")
        {
            instructions.Add(".method public static main([Ljava/lang/String;)V");
        }
        else
        {
            // TODO: Add parameters
            instructions.Add($".method public static {Symbol.Name}()V");
        }
        
        // Method prologue
        // TODO: Calculate stack size
        instructions.Add(".limit stack 100");
        instructions.Add(".limit locals 100");
        
        // Method body
        instructions.AddRange(Block.Translate());

        // Method epilogue
        if (Symbol.Name == "main" || Symbol.Type == TypeManager.DataType.Void)
        {
            instructions.Add("return");
        }
        
        // Method end
        instructions.Add(".end method");
        
        return instructions;
    }
}