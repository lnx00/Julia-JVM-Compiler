namespace Compiler.Core.IntermediateCode;

public class IntOperationInstruction : Instruction
{
    public enum Operation
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod
    }
    
    public override bool IsLeader => false;
    private Operation Op { get; }
    
    public IntOperationInstruction(Operation op)
    {
        Op = op;
    }
    
    public override string Translate()
    {
        return Op switch
        {
            Operation.Add => "\tiadd",
            Operation.Sub => "\tisub",
            Operation.Mul => "\timul",
            Operation.Div => "\tidiv",
            Operation.Mod => "\tirem",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}