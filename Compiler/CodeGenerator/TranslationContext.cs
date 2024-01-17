namespace Compiler.CodeGenerator;

public class TranslationContext
{
    public string Name { get; }
    
    public TranslationContext(string name)
    {
        Name = name;
    }
}