using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class StringConstNode : ExpressionNode
{
    public string Value { get; }
    public override TypeManager.DataType Type { get; }
    
    public StringConstNode(string value)
    {
        Value = value;
        Type = TypeManager.DataType.String;
    }

    public override List<string> Translate(TranslationContext ctx)
    {
        return new List<string>
        {
            $"\tldc {Value}"
        };
    }
}