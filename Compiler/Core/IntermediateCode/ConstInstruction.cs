using System.Globalization;

namespace Compiler.Core.IntermediateCode;

public class ConstInstruction : Instruction
{
    public override bool IsLeader => false;
    private string Value { get; }
    
    public ConstInstruction(string value)
    {
        Value = value;
    }
    
    public ConstInstruction(int value)
    {
        Value = value.ToString();
    }
    
    public ConstInstruction(double value)
    {
        Value = value.ToString("F", CultureInfo.InvariantCulture);;
    }
    
    public ConstInstruction(bool value)
    {
        Value = value ? "1" : "0";
    }
    
    public override string Translate()
    {
        return $"\tldc {Value}";
    }
}