namespace Compiler.Core.IntermediateCode;

// Only use this for rare instructions that are not covered by the other classes
public class CustomInstruction : Instruction
{
    private string Instruction { get; }
    private List<string> Parameters { get; }
    
    public CustomInstruction(string instruction, List<string> parameters)
    {
        Instruction = instruction;
        Parameters = parameters;
    }
    
    public override string Translate()
    {
        return $"{Instruction} {string.Join(" ", Parameters)}";
    }
}