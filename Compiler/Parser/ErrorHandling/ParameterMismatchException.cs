using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class ParameterMismatchException : ParserException
{
    private ParameterMismatchException(string message, int line, int position) : base(message, line, position)
    { }
    
    public static ParameterMismatchException Create(string funcName, ParserRuleContext ctx)
    {
        var msg = $"Parameter mismatch for function '{funcName}'";
        return new ParameterMismatchException(msg, ctx.Start.Line, ctx.Start.Column);
    }
}