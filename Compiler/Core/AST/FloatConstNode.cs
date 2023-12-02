using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class FloatConstNode : ExpressionNode
{
    public float Value { get; }
    public override TypeManager.DataType Type { get; }
    
    public FloatConstNode(float value)
    {
        Value = value;
        Type = TypeManager.DataType.Float64;
    }
}