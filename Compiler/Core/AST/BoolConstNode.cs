using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

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
        return new TranslationResult(new ConstInstruction(Value), 1);
    }
}