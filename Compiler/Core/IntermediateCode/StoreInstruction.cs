using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class StoreInstruction : Instruction
{
    public override bool IsLeader => false;
    private int Offset { get; }
    private TypeManager.DataType Type { get; }
    
    public StoreInstruction(int offset, TypeManager.DataType type)
    {
        Offset = offset;
        Type = type;
    }
    
    public override string Translate()
    {
        return Type switch
        {
            TypeManager.DataType.Integer => $"\tistore {Offset}",
            TypeManager.DataType.Bool => $"\tistore {Offset}",
            TypeManager.DataType.Float64 => $"\tfstore {Offset}",
            TypeManager.DataType.String => $"\tastore {Offset}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}