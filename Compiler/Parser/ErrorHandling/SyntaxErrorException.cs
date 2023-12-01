namespace Compiler.Parser.ErrorHandling;

public class SyntaxErrorException : ParserException
{
    public SyntaxErrorException(string message, int line, int position) : base(message, line, position) { }
}