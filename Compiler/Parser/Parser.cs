using Antlr4.Runtime;
using Compiler.Parser.ErrorHandling;

namespace Compiler.Parser;

public class Parser
{
    private readonly string _code;
    
    public Parser(string input)
    {
        _code = input;
    }
    
    public void Parse()
    {
        AntlrInputStream inputStream = new AntlrInputStream(_code);
        JuliaLexer juliaLexer = new JuliaLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(juliaLexer);
        JuliaParser juliaParser = new JuliaParser(commonTokenStream);
        
        // Handle errors
        juliaParser.AddErrorListener(new ErrorListener());

        var startContext = juliaParser.start();
        Visitor.Visitor visitor = new();
        visitor.Visit(startContext);
    }
}