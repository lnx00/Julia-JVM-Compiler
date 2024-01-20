namespace Compiler.Core.IntermediateCode;

public class ConstInstruction : Instruction
{
    public override bool IsLeader => false;
    private string Value { get; }
    
    public ConstInstruction(string value)
    {
        Value = value;
    }
    
    public override string Translate()
    {
        return $"\tldc {Value}";
    }
}