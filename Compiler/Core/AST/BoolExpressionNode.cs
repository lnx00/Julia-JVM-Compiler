using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class BoolExpressionNode : ExpressionNode
{
    public override TypeManager.DataType Type { get; } = TypeManager.DataType.Bool;
}