namespace Compiler.Parser.ErrorHandling;

public class SyntaxErrorException : Exception
{
    public int Line { get; }
    public int Position { get; }
    
    public SyntaxErrorException(string message, int line, int position) : base(message)
    {
        Line = line;
        Position = position;
    }
}