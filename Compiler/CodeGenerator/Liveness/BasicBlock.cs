using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class BasicBlock
{
    public List<Instruction> Instructions { get; }
    
    public BasicBlock(List<Instruction> instructions)
    {
        Instructions = instructions;
    }
}