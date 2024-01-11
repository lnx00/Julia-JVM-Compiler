using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class AddInstruction : Instruction
{
    private TypeManager.DataType Type { get; }
    
    public AddInstruction(TypeManager.DataType type)
    {
        Type = type;
    }
    
    public override string Translate()
    {
        switch (Type)
        {
            case TypeManager.DataType.Integer:
                return "iadd";
            
            case TypeManager.DataType.Float64:
                return "fadd";
            
            case TypeManager.DataType.String:
            case TypeManager.DataType.Bool:
            case TypeManager.DataType.Void:
            case TypeManager.DataType.Any:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}