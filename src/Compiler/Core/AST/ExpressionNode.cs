using Compiler.Core.Common;

namespace Compiler.Core.AST;

public abstract class ExpressionNode : INode
{
    public abstract TypeManager.DataType Type { get; }
}