using Compiler.Core.Common;

namespace Compiler.Core.IntermediateCode;

public class ArithmeticInstruction : Instruction
{
    public enum Operation
    {
        Add,
        Sub,
        Mul,
        Div,
        Mod,
        
        Neg,
        
        And,
        Or,
        Xor
    }
    
    public override bool IsLeader => false;
    private Operation Op { get; }
    private TypeManager.DataType Type { get; }
    
    public ArithmeticInstruction(Operation op, TypeManager.DataType type)
    {
        Op = op;
        Type = type;
    }
    
    public override string Translate()
    {
        return Op switch
        {
            Operation.Add => Type switch
            {
                TypeManager.DataType.Integer => "\tiadd",
                TypeManager.DataType.Float64 => "\tfadd",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.Sub => Type switch
            {
                TypeManager.DataType.Integer => "\tisub",
                TypeManager.DataType.Float64 => "\tfsub",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.Mul => Type switch
            {
                TypeManager.DataType.Integer => "\timul",
                TypeManager.DataType.Float64 => "\tfmul",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.Div => Type switch
            {
                TypeManager.DataType.Integer => "\tidiv",
                TypeManager.DataType.Float64 => "\tfdiv",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.Mod => Type switch
            {
                TypeManager.DataType.Integer => "\tirem",
                TypeManager.DataType.Float64 => "\tfrem",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.Neg => Type switch
            {
                TypeManager.DataType.Integer => "\tineg",
                TypeManager.DataType.Float64 => "\tfneg",
                _ => throw new ArgumentOutOfRangeException()
            },
            
            Operation.And => "\tiand",
            Operation.Or => "\tior",
            Operation.Xor => "\tixor",
            
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}