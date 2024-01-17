using Antlr4.Runtime;
using Compiler.Core.AST;
using Compiler.Core.StandardLibrary;
using Compiler.Core.SymbolTable;
using Compiler.Parser.ErrorHandling;
using Compiler.Parser.Visitor;

namespace Compiler.Parser;

public class Parser
{
    private readonly string _code;
    private readonly ErrorListener _errorListener = new();
    private readonly SymbolTable _symbolTable = new();
    
    public Parser(string input)
    {
        _code = input;
    }
    
    public BlockNode Parse()
    {
        // Initialize lexer
        var inputStream = new AntlrInputStream(_code);
        var juliaLexer = new JuliaLexer(inputStream);
        juliaLexer.RemoveErrorListeners();
        
        // Initialize parser
        var commonTokenStream = new CommonTokenStream(juliaLexer);
        var juliaParser = new JuliaParser(commonTokenStream);
        juliaParser.RemoveErrorListeners();
        juliaParser.AddErrorListener(_errorListener);
        
        // Initialize visitor
        var startContext = juliaParser.start();
        
        var globalVisitor = new GlobalVisitor(_symbolTable);
        globalVisitor.Visit(startContext);
        
        var juliaVisitor = new JuliaVisitor(_symbolTable);
        return juliaVisitor.Visit(startContext) as BlockNode ?? throw new Exception("Error parsing code");
    }
    
    public SymbolTable GetSymbolTable()
    {
        return _symbolTable;
    }
}