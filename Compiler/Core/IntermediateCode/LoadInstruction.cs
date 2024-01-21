using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class LoadInstruction : Instruction
{
    public override bool IsLeader => false;
    public int Offset { get; }
    private TypeManager.DataType Type { get; }
    
    public LoadInstruction(int offset, TypeManager.DataType type)
    {
        Offset = offset;
        Type = type;
    }
    
    public override string Translate()
    {
        return Type switch
        {
            TypeManager.DataType.Integer => $"\tiload {Offset}",
            TypeManager.DataType.Bool => $"\tiload {Offset}",
            TypeManager.DataType.Float64 => $"\tfload {Offset}",
            TypeManager.DataType.String => $"\taload {Offset}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}