using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class IntegerConstNode : ExpressionNode
{
    public int Value { get; }
    public override TypeManager.DataType Type { get; }
    
    public IntegerConstNode(int value)
    {
        Value = value;
        Type = TypeManager.DataType.Integer;
    }

    public override List<string> Translate(TranslationContext ctx)
    {
        return new List<string>
        {
            $"\tldc {Value}"
        };
    }
}