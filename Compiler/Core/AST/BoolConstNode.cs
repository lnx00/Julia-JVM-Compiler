using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class BoolConstNode : ExpressionNode
{
    public bool Value { get; }
    public override TypeManager.DataType Type { get; }
    
    public BoolConstNode(bool value)
    {
        Value = value;
        Type = TypeManager.DataType.Bool;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        var value = Value ? 1 : 0;
        
        return new TranslationResult($"\tldc {value}", 1);
    }
}