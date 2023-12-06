using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class SyntaxErrorException : ParserException
{
    public SyntaxErrorException(string message, int line, int position) : base(message, line, position) { }
    
    public static SyntaxErrorException Create(ParserRuleContext ctx)
    {
        var msg = $"Unknown syntax error";
        return Create(msg, ctx);
    }
    
    public static SyntaxErrorException Create(string message, ParserRuleContext ctx)
    {
        return new SyntaxErrorException(message, ctx.Start.Line, ctx.Start.Column);
    }
}