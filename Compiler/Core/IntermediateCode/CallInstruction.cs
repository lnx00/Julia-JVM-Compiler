using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.IntermediateCode;

public class CallInstruction : Instruction
{
    public enum InvocationType
    {
        Static,
        Virtual,
        Special
    }
    
    private InvocationType Type { get; }
    private FunctionSymbol Function { get; }
    
    public CallInstruction(InvocationType type, FunctionSymbol function)
    {
        Type = type;
        Function = function;
    }

    public override string Translate()
    {
        return Type switch
        {
            InvocationType.Static => $"invokestatic {Function.Name}",
            InvocationType.Virtual => $"invokevirtual {Function.Name}",
            InvocationType.Special => $"invokespecial {Function.Name}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}