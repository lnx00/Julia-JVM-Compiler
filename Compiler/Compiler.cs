using Compiler.Parser.ErrorHandling;

namespace Compiler;

public class Compiler
{
    private readonly Parser.Parser _parser;
    
    public Compiler(string sourceCode)
    {
        _parser = new Parser.Parser(sourceCode);
    }

    private void Parse()
    {
        try
        {
            _parser.Parse();
        }
        catch (ParserException e)
        {
            Console.WriteLine($"Error at line {e.Line}, position {e.Position}: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unknown parser error: {e.Message}");
        }
    }
    
    public void Compile()
    {
        Parse();
    }
    
    public void LivenessAnalysis()
    {
        Console.WriteLine("Not implemented yet");
    }
}