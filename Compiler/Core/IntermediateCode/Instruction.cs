namespace Compiler.Core.IntermediateCode;

public enum InstructionOpCode
{
    LoadInt,
    Add
}

public abstract class Instruction
{
    public InstructionOpCode OpCode { get; }

    protected Instruction(InstructionOpCode opCode)
    {
        OpCode = opCode;
    }
}