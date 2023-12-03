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
        var errorListener = new ErrorListener();
        
        // Initialize lexer
        var inputStream = new AntlrInputStream(_code);
        var juliaLexer = new JuliaLexer(inputStream);
        juliaLexer.RemoveErrorListeners();
        
        // Initialize parser
        var commonTokenStream = new CommonTokenStream(juliaLexer);
        var juliaParser = new JuliaParser(commonTokenStream);
        juliaParser.RemoveErrorListeners();
        juliaParser.AddErrorListener(errorListener);
        
        // Initialize visitor
        var startContext = juliaParser.start();
        var juliaVisitor = new JuliaVisitor();
        
        juliaVisitor.Visit(startContext);
    }
}