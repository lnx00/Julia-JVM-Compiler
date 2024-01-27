using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class LivenessNode
{
    public List<LivenessNode> Predecessors { get; } = new();
    public List<LivenessNode> Successors { get; } = new();
    
    public HashSet<int> Defs { get; } = new();
    public HashSet<int> Uses { get; } = new();
    
    public HashSet<int> LiveIn { get; } = new();
    public HashSet<int> LiveOut { get; } = new();
    
    public string? Label { get; set; }
    public string? TargetLabel { get; set; }

    public LivenessNode(string label)
    {
        Label = label;
    }
    
    public LivenessNode(Instruction instruction)
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
                Uses.Add(load.Offset);
                break;
            }
            
            case LabelInstruction label:
            {
                Label = label.Label;
                break;
            }
            
            case BranchInstruction branch:
            {
                TargetLabel = branch.Label.Label;
                break;
            }
            
            case ReturnInstruction ret:
            {
                TargetLabel = "@exit";
                break;
            }
        }
    }
}