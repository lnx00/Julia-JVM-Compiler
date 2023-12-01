namespace Compiler.Parser.ErrorHandling;

public class ParserException : Exception
{
    public int Line { get; }
    public int Position { get; }
    public override string Message { get; }

    protected ParserException(string message, int line, int position) : base(message)
    {
        Line = line;
        Position = position;
        Message = message;
    }
}