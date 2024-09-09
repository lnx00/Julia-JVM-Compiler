namespace Compiler.Core.IntermediateCode;

public class SeparatorInstruction : Instruction
{
    public override bool IsLeader => false;
    private int Size { get; }
    
    public SeparatorInstruction()
    {
        Size = 1;
    }
    
    public SeparatorInstruction(int size)
    {
        Size = size;
    }
    
    public override string Translate()
    {
        return string.Concat(Enumerable.Repeat("\n", Size));
    }
}