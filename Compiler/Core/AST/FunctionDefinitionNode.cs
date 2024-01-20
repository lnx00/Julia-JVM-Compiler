using System.Text;
using Compiler.CodeGenerator;
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

    public override TranslationResult Translate(TranslationContext ctx)
    {
        string returnType = TypeManager.GetJasminType(Symbol.Type);
        //string parameterTypes = Symbol.Parameters.Aggregate("", (current, parameter) => current + TypeManager.GetJasminType(parameter.Value));
        string parameterTypes = Symbol.Parameters.Aggregate(string.Empty, (current, param) => current + TypeManager.GetJasminType(param.Type));
        var result = Block.Translate(ctx);
        
        // Method prologue
        List<string> instructions = new()
        {
            $"; Function definition for '{Symbol.Name}'",
            $".method public static {Symbol.GetMangledName()}({parameterTypes}){returnType}",
            $"\t.limit stack {result.StackSize}",
            $"\t.limit locals {Symbol.VariableCount}"
        };

        // Method body
        instructions.AddRange(result.Instructions);
        
        // Method end
        instructions.Add(".end method");
        instructions.Add(string.Empty);
        
        return new TranslationResult(instructions, result.StackSize);
    }
}