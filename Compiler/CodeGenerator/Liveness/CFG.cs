using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class CFG
{
    public List<LivenessNode> Nodes { get; } = new();
    
    public LivenessNode Entry { get; } = new("@entry");
    public LivenessNode Exit { get; } = new("@exit");

    public CFG(List<Instruction> instructions)
    {
        // Entry block
        Nodes.Add(Entry);
        var lastNode = Entry;
        
        // Split blocks on labels and branches
        foreach (var instruction in instructions)
        {
            var node = new LivenessNode(instruction);
            node.Predecessors.Add(lastNode);
            lastNode.Successors.Add(node);
            
            Nodes.Add(node);
            lastNode = node;
        }
        
        // Exit block
        lastNode.Successors.Add(Exit);
        Exit.Predecessors.Add(lastNode);
        Nodes.Add(Exit);
        
        // Link blocks
        foreach (var node in Nodes)
        {
            if (node.TargetLabel is null) continue;
            
            var targetNode = GetNodeByLabel(node.TargetLabel);
            if (targetNode is null) continue;
            
            node.Successors.Add(targetNode);
            targetNode.Predecessors.Add(node);
        }
    }

    private LivenessNode? GetNodeByLabel(string label)
    {
        return Nodes.FirstOrDefault(node => node.Label == label);
    }

    public int Analyze()
    {
        while (true)
        {
            bool hasChanged = false;
            
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                var node = Nodes[i];
                
                HashSet<int> liveOutBackup = new(node.LiveOut);
                HashSet<int> liveInBackup = new(node.LiveIn);
                
                // Update liveOut
                foreach (var successor in node.Successors)
                {
                    node.LiveOut.UnionWith(successor.LiveIn);
                }
                
                // Update liveIn
                HashSet<int> liveOutWithoutDefs = new(node.LiveOut);
                liveOutWithoutDefs.ExceptWith(node.Defs);
                node.LiveIn.UnionWith(node.Uses);
                node.LiveIn.UnionWith(liveOutWithoutDefs);
                
                // Check if liveIn or liveOut changed
                if (!liveOutBackup.SetEquals(node.LiveOut) || !liveInBackup.SetEquals(node.LiveIn))
                {
                    hasChanged = true;
                }
            }
            
            if (!hasChanged) { break; }
        }
        
        // Calculate max amount of live variables
        return Nodes.Select(block => block.LiveIn.Count).Max();
    }
}