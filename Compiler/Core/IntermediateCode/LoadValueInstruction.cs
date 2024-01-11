namespace Compiler.Core.IntermediateCode;

public class LoadValueInstruction : Instruction
{
    private int Register { get; }
    
    public LoadValueInstruction(int register)
    {
        Register = register;
    }
    
    public override string Translate()
    {
        return $"aload {Register}";
    }
}