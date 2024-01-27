using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator;

public class TranslationResult
{
    public List<Instruction> Instructions { get; }
    public int StackSize { get; set; }

    public TranslationResult(List<Instruction> instructions, int stackSize)
    {
        Instructions = instructions;
        StackSize = stackSize;
    }
    
    public TranslationResult(List<Instruction> instructions, int leftStackSize, int rightStackSize)
    {
        Instructions = instructions;
        StackSize = Math.Max(leftStackSize, rightStackSize);
        
        if (leftStackSize == rightStackSize)
        {
            StackSize++;
        }
    }
    
    public TranslationResult(Instruction instruction, int stackSize)
    {
        Instructions = new List<Instruction> { instruction };
        StackSize = stackSize;
    }
}