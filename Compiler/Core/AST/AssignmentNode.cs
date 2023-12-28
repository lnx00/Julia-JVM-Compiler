namespace Compiler.Core.AST;

public class AssignmentNode : INode
{
    public string Name { get; }
    public ExpressionNode Value { get; }

    public AssignmentNode(string name, ExpressionNode value)
    {
        Name = name;
        Value = value;
    }
}