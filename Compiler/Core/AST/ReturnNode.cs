using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

namespace Compiler.Core.AST;

public class ReturnNode : INode
{
    public ExpressionNode? Value { get; }

    public ReturnNode(ExpressionNode? value)
    {
        Value = value;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        // Void
        if (Value is null)
        {
            return new TranslationResult(new ReturnInstruction(TypeManager.DataType.Void), 0);
        }

        // Non-void
        var result = Value.Translate(ctx);
        List<Instruction> instructions = result.Instructions;
        
        instructions.Add(new ReturnInstruction(Value.Type));
        
        return new TranslationResult(instructions, result.StackSize);
    }
}