using Antlr4.Runtime;
using Compiler.Parser.ErrorHandling;
using Compiler.Parser.Visitor;

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
        // Initialize lexer, parser and visitor
        var inputStream = new AntlrInputStream(_code);
        var juliaLexer = new JuliaLexer(inputStream);
        var commonTokenStream = new CommonTokenStream(juliaLexer);
        var juliaParser = new JuliaParser(commonTokenStream);
        
        // Handle errors
        juliaParser.AddErrorListener(new ErrorListener());
        
        // Visit start rule
        var startContext = juliaParser.start();
        var juliaVisitor = new JuliaVisitor();
        juliaVisitor.Visit(startContext);
    }
}