using Compiler.Core.AST;
using Compiler.Parser.ErrorHandling;

namespace Compiler;

public class Compiler
{
    private readonly Parser.Parser _parser;
    private readonly CodeGenerator.CodeGenerator _codeGenerator;
    
    public Compiler(string sourceCode)
    {
        _parser = new Parser.Parser(sourceCode);
        _codeGenerator = new CodeGenerator.CodeGenerator();
    }

    private BlockNode Parse()
    {
        return _parser.Parse();
    }
    
    private List<string> GenerateCode(BlockNode ast)
    {
        return _codeGenerator.Generate(ast);
    }
    
    public void Compile()
    {
        var ast = Parse();
        var code = GenerateCode(ast);
        
        // Print the generated code (for debugging purposes)
        foreach (var instruction in code)
        {
            Console.WriteLine(instruction);
        }
    }
    
    public void LivenessAnalysis()
    {
        var ast = Parse();
        Console.WriteLine($"Registers: {_parser.GetSymbolTable().VariableCount}");
    }
}