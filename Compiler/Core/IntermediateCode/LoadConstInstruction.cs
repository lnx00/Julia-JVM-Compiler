using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class LoadConstInstruction : Instruction
{
    private object Value { get; }
    private TypeManager.DataType Type { get; }
    
    public LoadConstInstruction(object value, TypeManager.DataType type)
    {
        Value = value;
        Type = type;
    }
    
    public override string Translate()
    {
        switch (Type)
        {
            case TypeManager.DataType.Integer:
            case TypeManager.DataType.Float64:
                return $"ldc {Value}";
            
            case TypeManager.DataType.String:
                return $"ldc \"{Value}\"";
            
            case TypeManager.DataType.Bool:
                bool boolValue = (bool) Value;
                return $"ldc {boolValue.ToString().ToLower()}";
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}