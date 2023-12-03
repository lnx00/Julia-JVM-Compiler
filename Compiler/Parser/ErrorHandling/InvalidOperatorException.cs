using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class InvalidOperatorException : ParserException
{
    public string Op { get; }
    
    protected InvalidOperatorException(string message, int line, int position, string op) : base(message, line, position)
    {
        Op = op;
    }
    
    public static InvalidOperatorException Create(string op, ParserRuleContext ctx)
    {
        var msg = $"Invalid operation {op}";
        return new InvalidOperatorException(msg, ctx.Start.Line, ctx.Start.Column, op);
    }
}