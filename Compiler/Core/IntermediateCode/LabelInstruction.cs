namespace Compiler.Core.IntermediateCode;

public class LabelInstruction : Instruction
{
    public override bool IsLeader => true;
    public string Label { get; }

    public LabelInstruction(string label)
    {
        Label = label;
    }

    public override string Translate()
    {
        return $"{Label}:";
    }

    public override string ToString()
    {
        return Label;
    }
}