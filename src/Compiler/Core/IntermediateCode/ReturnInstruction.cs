using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class ReturnInstruction : Instruction
{
    public override bool IsLeader => true;
    public TypeManager.DataType Type { get; }
    
    public ReturnInstruction(TypeManager.DataType type)
    {
        Type = type;
    }
    
    public override string Translate()
    {
        return Type switch
        {
            TypeManager.DataType.Void => "\treturn",
            TypeManager.DataType.Integer => "\tireturn",
            TypeManager.DataType.Float64 => "\tfreturn",
            TypeManager.DataType.String => "\tareturn",
            TypeManager.DataType.Bool => "\tireturn",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}