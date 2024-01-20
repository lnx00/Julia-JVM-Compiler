using System.Globalization;
using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

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
        return new TranslationResult(new ConstInstruction(Value), 1);
    }
}