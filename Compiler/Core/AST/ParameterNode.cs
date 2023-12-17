using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class ParameterNode : INode
{
    public string Name { get; }
    public TypeManager.DataType Type { get; }

    public ParameterNode(string name, TypeManager.DataType type)
    {
        Name = name;
        Type = type;
    }
}