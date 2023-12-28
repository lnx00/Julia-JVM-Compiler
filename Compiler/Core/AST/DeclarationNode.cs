using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class DeclarationNode : INode
{
    public string Name { get; }
    public TypeManager.DataType Type { get; }
    public ExpressionNode Value { get; }

    public DeclarationNode(string name, TypeManager.DataType type, ExpressionNode value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}