namespace Compiler.Core.IntermediateCode;

public class InvokeInstruction : Instruction
{
    public enum InvokeType
    {
        Static,
        Virtual,
        Special
    }
    
    public override bool IsLeader => false;
    private string FunctionSignature { get; }
    private InvokeType Type { get; }
    
    public InvokeInstruction(string functionSignature, InvokeType type)
    {
        FunctionSignature = functionSignature;
        Type = type;
    }
    
    public override string Translate()
    {
        return Type switch
        {
            InvokeType.Static => $"\tinvokestatic {FunctionSignature}",
            InvokeType.Virtual => $"\tinvokevirtual {FunctionSignature}",
            InvokeType.Special => $"\tinvokespecial {FunctionSignature}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}