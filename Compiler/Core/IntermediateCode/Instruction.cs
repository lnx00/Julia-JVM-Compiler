namespace Compiler.Core.IntermediateCode;

public abstract class Instruction
{
    public abstract bool IsLeader { get; }
    public abstract string Translate();
}