namespace Compiler.CodeGenerator;

public class TranslationResult
{
    public List<string> Instructions { get; }
    public int StackSize { get; set; }

    public TranslationResult(List<string> instructions, int stackSize)
    {
        Instructions = instructions;
        StackSize = stackSize;
    }
    
    public TranslationResult(List<string> instructions, int leftStackSize, int rightStackSize)
    {
        Instructions = instructions;
        StackSize = Math.Max(leftStackSize, rightStackSize);
        
        if (leftStackSize == rightStackSize)
        {
            StackSize++;
        }
    }
    
    public TranslationResult(string instruction, int stackSize)
    {
        Instructions = new List<string> { instruction };
        StackSize = stackSize;
    }
}