using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class ErrorListener : BaseErrorListener
{
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        throw new SyntaxErrorException(msg, line, charPositionInLine);
    }
}