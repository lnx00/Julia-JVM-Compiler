using Compiler.Core.IntermediateCode;

namespace Compiler.Core.Common;

public static class LabelManager
{
    private static int _labelCounter = 0;
    
    public static string GetLabel()
    {
        return $"L{_labelCounter++}";
    }
    
    public static LabelInstruction GetLabel(string label)
    {
        return new LabelInstruction($"{label}{_labelCounter++}");
    }
}