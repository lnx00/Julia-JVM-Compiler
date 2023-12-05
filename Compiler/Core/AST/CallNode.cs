using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class CallNode : ExpressionNode
{
    public string Name { get; }
    public override TypeManager.DataType Type { get; }
    public List<ExpressionNode> Arguments { get; }
    
    public CallNode(string name, List<ExpressionNode> arguments, TypeManager.DataType type)
    {
        Name = name;
        Arguments = arguments;
        Type = type;
    }
}