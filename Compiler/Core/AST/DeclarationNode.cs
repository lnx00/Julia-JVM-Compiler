using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class DeclarationNode : INode
{
    public VariableSymbol Symbol { get; }
    public ExpressionNode Value { get; }

    public DeclarationNode(VariableSymbol symbol, ExpressionNode value)
    {
        Symbol = symbol;
        Value = value;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<Instruction> instructions = new();
        var result = Value.Translate(ctx);

        instructions.AddRange(result.Instructions);
        instructions.Add(new StoreInstruction(Symbol.Offset, Value.Type));

        return new TranslationResult(instructions, result.StackSize);
    }
}