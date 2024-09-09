namespace Compiler.Core.IntermediateCode;

public class CustomInstruction : Instruction
{
    public override bool IsLeader => false;
    private string Instruction { get; }

    public CustomInstruction(string instruction)
    {
        Instruction = instruction;
    }

    public override string Translate()
    {
        return Instruction;
    }
}