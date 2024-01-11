namespace Compiler.Core.IntermediateCode;

public class LabelInstruction : Instruction
{
    private string Label { get; }

    public LabelInstruction(string label)
    {
        Label = label;
    }

    public override string Translate()
    {
        return $"{Label}:";
    }
}