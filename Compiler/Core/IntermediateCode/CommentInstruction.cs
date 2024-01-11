namespace Compiler.Core.IntermediateCode;

public class CommentInstruction : Instruction
{
    private string Comment { get; }

    public CommentInstruction(string comment)
    {
        Comment = comment;
    }

    public override string Translate()
    {
        return $"; {Comment}";
    }
}