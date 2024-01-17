using System.Text;
using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class FunctionDefinitionNode : INode
{
    public FunctionSymbol Symbol { get; }
    public BlockNode Block { get; }
   // public ParameterNode Parameters { get; }

    public FunctionDefinitionNode(FunctionSymbol symbol, BlockNode block)
    {
        Symbol = symbol;
        Block = block;
    }

    public override List<string> Translate()
    {
        string returnType = TypeManager.GetJasminType(Symbol.Type);
        //string parameterTypes = Symbol.Parameters.Aggregate("", (current, parameter) => current + TypeManager.GetJasminType(parameter.Value));
        string parameterTypes = Symbol.Parameters.Aggregate(string.Empty, (current, param) => current + TypeManager.GetJasminType(param.Type));

        // Method prologue
        List<string> instructions = new()
        {
            $"; Function definition for '{Symbol.Name}'",
            $".method public static {Symbol.GetMangledName()}({parameterTypes}){returnType}",
            "\t.limit stack 100",
            $"\t.limit locals {Symbol.VariableCount}"
        };

        // Method body
        instructions.AddRange(Block.Translate());
        
        // Method end
        instructions.Add(".end method");
        instructions.Add(string.Empty);
        
        return instructions;
    }
}