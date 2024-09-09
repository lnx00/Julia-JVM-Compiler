using System.Text;
using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;
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
        List<Instruction> instructions = new()
        {
            //$"; Function definition for '{Symbol.Name}'",
            new CommentInstruction($"Function definition for '{Symbol.Name}'"),
            new CustomInstruction($".method public static {Symbol.GetMangledName()}({parameterTypes}){returnType}"),
            new CustomInstruction($"\t.limit stack {result.StackSize}"),
            new CustomInstruction($"\t.limit locals {Symbol.VariableCount}")
        };

        // Method body
        instructions.AddRange(result.Instructions);
        
        // Return statement
        instructions.Add(new ReturnInstruction(Symbol.Type));
        
        // Method end
        instructions.Add(new CustomInstruction(".end method"));
        instructions.Add(new CustomInstruction("")); // TODO: Hack to add a new line
        
        return new TranslationResult(instructions, result.StackSize);
    }
}