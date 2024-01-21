using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class ControlFlowGraph
{
    public List<BasicBlock> Blocks { get; }
    
    public ControlFlowGraph(List<BasicBlock> blocks)
    {
        Blocks = blocks;
    }
    
    public static ControlFlowGraph Generate(List<Instruction> instructions)
    {
        var blocks = new List<BasicBlock>();
        var block = new BasicBlock(new List<Instruction>());
        
        foreach (var instruction in instructions)
        {
            block.Instructions.Add(instruction);
            
            // New leader
            if (instruction.IsLeader)
            {
                blocks.Add(block);
                block = new BasicBlock(new List<Instruction>());
            }
        }
        
        blocks.Add(block);
        return new ControlFlowGraph(blocks);
    }
}