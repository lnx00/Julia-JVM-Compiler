namespace Compiler.CodeGenerator.Liveness;

public class GraphNode
{
    public HashSet<GraphNode> AdjacentNodes { get; } = new();
    public int Color { get; set; } = -1;
}