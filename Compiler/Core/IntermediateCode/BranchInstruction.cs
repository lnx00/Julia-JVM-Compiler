namespace Compiler.Core.IntermediateCode;

public class BranchInstruction : Instruction
{
    public enum Condition
    {
        None,
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
    
    public override bool IsLeader => true;
    private Condition Cond { get; }
    private string Label { get; }
    
    public BranchInstruction(Condition cond, string label)
    {
        Cond = cond;
        Label = label;
    }
    
    public override string Translate()
    {
        return Cond switch
        {
            Condition.None => $"\tgoto {Label}",
            Condition.Equal => $"\tifeq {Label}",
            Condition.NotEqual => $"\tifne {Label}",
            Condition.GreaterThan => $"\tifgt {Label}",
            Condition.GreaterThanOrEqual => $"\tifge {Label}",
            Condition.LessThan => $"\tiflt {Label}",
            Condition.LessThanOrEqual => $"\tifle {Label}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}