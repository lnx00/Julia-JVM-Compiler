using Antlr4.Runtime;

namespace Compiler.Parser.ErrorHandling;

public class UndefinedFuncException : ParserException
{
    private UndefinedFuncException(string message, int line, int position) : base(message, line, position) { }
    
    public static UndefinedFuncException Create(string funcName, ParserRuleContext context)
    {
        return new UndefinedFuncException($"Undefined function '{funcName}'", context.Start.Line, context.Start.Column);
    }
}