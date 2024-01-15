using System.Text;
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
        string returnType = TypeManager.GetJasminType(Symbol.Type);
        string parameterTypes = Parameters.Parameters.Aggregate("", (current, parameter) => current + TypeManager.GetJasminType(parameter.Value));

        // Method prologue
        List<string> instructions = new()
        {
            $"; Function definition for '{Symbol.Name}'",
            $".method public static {Symbol.GetMangledName()}({parameterTypes}){returnType}",
            ".limit stack 100",
            ".limit locals 100"
        };

        // Method body
        instructions.AddRange(Block.Translate());
        
        // Method end
        instructions.Add(".end method");
        instructions.Add(string.Empty);
        
        return instructions;
    }
}