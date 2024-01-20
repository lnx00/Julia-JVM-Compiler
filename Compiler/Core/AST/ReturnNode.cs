using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class ReturnNode : INode
{
    public ExpressionNode? Value { get; }

    public ReturnNode(ExpressionNode? value)
    {
        Value = value;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        // Void
        if (Value is null)
        {
            return new TranslationResult("\treturn", 0);
        }

        // Non-void
        var result = Value.Translate(ctx);
        List<string> instructions = result.Instructions;

        switch (Value.Type)
        {
            case TypeManager.DataType.Bool:
            case TypeManager.DataType.Integer:
                instructions.Add("\tireturn");
                break;
            
            case TypeManager.DataType.Float64:
                instructions.Add("\tfreturn");
                break;
            
            case TypeManager.DataType.String:
                instructions.Add("\tareturn");
                break;

            case TypeManager.DataType.Void:
                instructions.Add("\treturn");
                break;
                
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return new TranslationResult(instructions, result.StackSize);
    }
}