using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class ReturnNode : INode
{
    public ExpressionNode? Value { get; }

    public ReturnNode(ExpressionNode? value)
    {
        Value = value;
    }

    public override List<string> Translate()
    {
        // Void
        if (Value is null)
        {
            return new List<string>
            {
                "return"
            };
        }

        // Non-void
        List<string> instructions = Value.Translate();

        switch (Value.Type)
        {
            case TypeManager.DataType.Bool:
            case TypeManager.DataType.Integer:
                instructions.Add("ireturn");
                break;
            
            case TypeManager.DataType.Float64:
                instructions.Add("dreturn");
                break;
            
            case TypeManager.DataType.String:
                instructions.Add("areturn");
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return instructions;
    }
}