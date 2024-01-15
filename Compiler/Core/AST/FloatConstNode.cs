using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class FloatConstNode : ExpressionNode
{
    public double Value { get; }
    public override TypeManager.DataType Type { get; }
    
    public FloatConstNode(double value)
    {
        Value = value;
        Type = TypeManager.DataType.Float64;
    }

    public override List<string> Translate()
    {
        return new List<string>
        {
            $"\tldc {Value}"
        };
    }
}