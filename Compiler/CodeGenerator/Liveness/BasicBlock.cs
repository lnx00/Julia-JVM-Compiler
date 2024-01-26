using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class BasicBlock
{
    public List<Instruction> Instructions { get; } = new();

    public List<BasicBlock> Predecessors { get; } = new();
    public List<BasicBlock> Successors { get; } = new();
    
    public string Label { get; set; } = string.Empty;
    public string? Target { get; set; }
    
    public HashSet<int> Defs { get; } = new();
    public HashSet<int> Uses { get; } = new();
    
    public HashSet<int> LiveIn { get; } = new();
    public HashSet<int> LiveOut { get; } = new();
    
    public BasicBlock() { }
    
    public BasicBlock(BasicBlock predecessor)
    {
        Predecessors.Add(predecessor);
    }
    
    public void Analyze()
    {
        // Initialize our Defs and Uses
        foreach (var instruction in Instructions)
        {
            switch (instruction)
            {
                case StoreInstruction store:
                {
                    Defs.Add(store.Offset);
                    break;
                }

                case LoadInstruction load:
                {
                    if (!Defs.Contains(load.Offset))
                    {
                        Uses.Add(load.Offset);
                    }
                    break;
                }
            }
        }
    }
}