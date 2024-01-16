namespace Compiler.Core.Common;

public static class LabelManager
{
    private static int _labelCounter = 0;
    
    public static string GetLabel()
    {
        return $"L{_labelCounter++}";
    }
    
public static string GetLabel(string label)
    {
        return $"{label}{_labelCounter++}";
    }
}