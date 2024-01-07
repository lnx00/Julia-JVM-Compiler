using Antlr4.Runtime;
using Compiler.Core.AST;
using Compiler.Core.SymbolTable;
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
    
    public BlockNode Parse()
    {
        var errorListener = new ErrorListener();
        var symbolTable = new SymbolTable();
        
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
        
        var globalVisitor = new GlobalVisitor(symbolTable);
        globalVisitor.Visit(startContext);
        
        var juliaVisitor = new JuliaVisitor(symbolTable);
        return juliaVisitor.Visit(startContext) as BlockNode ?? throw new Exception("Error parsing code");
    }
}