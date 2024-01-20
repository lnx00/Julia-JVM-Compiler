namespace Compiler.Core.IntermediateCode;

public class FloatOperationInstruction : Instruction
{
    public enum Operation
    {
        Add,
        Sub,
        Mul,
        Div
    }
    
    public override bool IsLeader => false;
    private Operation Op { get; }
    
    public FloatOperationInstruction(Operation op)
    {
        Op = op;
    }
    
    public override string Translate()
    {
        return Op switch
        {
            Operation.Add => "\tfadd",
            Operation.Sub => "\tfsub",
            Operation.Mul => "\tfmul",
            Operation.Div => "\tfdiv",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}