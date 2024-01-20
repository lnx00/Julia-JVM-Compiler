using System.Globalization;
using Compiler.CodeGenerator;
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

    public override TranslationResult Translate(TranslationContext ctx)
    {
        string value = Value.ToString("F", CultureInfo.InvariantCulture);
        return new TranslationResult( $"\tldc {value}", 1);
    }
}