using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class IdentifierNode : ExpressionNode
{
    public string Name { get; }
    public override TypeManager.DataType Type { get; }
    
    public IdentifierNode(string name, TypeManager.DataType type)
    {
        Name = name;
        Type = type;
    }

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}