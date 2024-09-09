using Antlr4.Runtime;
using Compiler.Core.Common;

namespace Compiler.Parser.ErrorHandling;

public class TypeMismatchException : ParserException
{
    public TypeManager.DataType A { get; }
    public TypeManager.DataType B { get; }

    private TypeMismatchException(string message, int line, int position, TypeManager.DataType a, TypeManager.DataType b) : base(message, line, position)
    {
        A = a;
        B = b;
    }
    
    public static TypeMismatchException Create(TypeManager.DataType a, TypeManager.DataType b, ParserRuleContext ctx)
    {
        var msg = $"Incompatible types {a} and {b}";
        return new TypeMismatchException(msg, ctx.Start.Line, ctx.Start.Column, a, b);
    }
}