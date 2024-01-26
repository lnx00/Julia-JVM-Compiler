using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class BasicBlock
{
    public List<Instruction> Instructions { get; } = new();

    public List<BasicBlock> Predecessors { get; } = new();
    public List<BasicBlock> Successors { get; } = new();
    
    public string Label { get; set; } = string.Empty;
    public string? Target { get; set; }
    
    public BasicBlock() { }
    
    public BasicBlock(BasicBlock predecessor)
    {
        Predecessors.Add(predecessor);
    }
}