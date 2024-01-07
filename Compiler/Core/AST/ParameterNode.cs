using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class ParameterNode : INode
{
    public Dictionary<string, TypeManager.DataType> Parameters { get; } = new();

    public ParameterNode()
    {
    }
}