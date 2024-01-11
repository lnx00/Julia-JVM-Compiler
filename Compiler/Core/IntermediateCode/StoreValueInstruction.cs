namespace Compiler.Core.IntermediateCode;

public class StoreValueInstruction : Instruction
{
    private int Register { get; }
    
    public StoreValueInstruction(int register)
    {
        Register = register;
    }
    
    public override string Translate()
    {
        return $"astore {Register}";
    }
}