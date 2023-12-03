using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class UndefinedVarException : ParserException
{
    protected UndefinedVarException(string message, int line, int position) : base(message, line, position)
    { }
    
    public static UndefinedVarException Create(string varName, ParserRuleContext ctx)
    {
        var msg = $"Undefined variable {varName}";
        return new UndefinedVarException(msg, ctx.Start.Line, ctx.Start.Column);
    }
}