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
        // Calculate liveIn and liveOut
        PerformLiveness();

        // Build interference graph
        Dictionary<int, GraphNode> graph = BuildInterferenceGraph();

        // Color graph
        var colorCount = ColorGraph(graph);

        return colorCount;
    }

    private void PerformLiveness()
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
    }

    private Dictionary<int, GraphNode> BuildInterferenceGraph()
    {
        Dictionary<int, GraphNode> graph = new();
        
        // Add nodes for each variable
        foreach (var node in Nodes)
        {
            foreach (var def in node.Defs)
            {
                if (!graph.ContainsKey(def))
                {
                    graph.Add(def, new GraphNode());
                }
            }
        }
        
        // Add edges
        foreach (var node in Nodes)
        {
            foreach (var def in node.Defs)
            {
                foreach (var liveOut in node.LiveOut.Where(liveOut => def != liveOut))
                {
                    graph[def].AdjacentNodes.Add(graph[liveOut]);
                    graph[liveOut].AdjacentNodes.Add(graph[def]);
                }
            }
        }

        return graph;
    }

    private static int ColorGraph(Dictionary<int, GraphNode> graph)
    {
        int colorCount = 0;
        foreach (var node in graph.Values)
        {
            HashSet<int> usedColors = new();
            foreach (var adjacentNode in node.AdjacentNodes)
            {
                if (adjacentNode.Color != -1)
                {
                    usedColors.Add(adjacentNode.Color);
                }
            }

            for (int i = 0; i < colorCount; i++)
            {
                if (!usedColors.Contains(i))
                {
                    node.Color = i;
                    break;
                }
            }

            if (node.Color == -1)
            {
                node.Color = colorCount;
                colorCount++;
            }
        }

        return colorCount;
    }
}