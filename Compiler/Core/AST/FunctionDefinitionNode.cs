using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class FunctionDefinitionNode : INode
{
    public string Name { get; }
    public TypeManager.DataType Type { get; }
    public BlockNode Block { get; }
    public ParameterNode Parameters { get; }

    public FunctionDefinitionNode(string name, TypeManager.DataType type, BlockNode block, ParameterNode parameters)
    {
        Name = name;
        Type = type;
        Block = block;
        Parameters = parameters;
    }
}