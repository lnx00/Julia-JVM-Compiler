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
    
    public string Compile()
    {
        var ast = Parse();
        var instructions = GenerateCode(ast);
        var code = string.Join("\n", instructions);

        return code;
    }
    
    public int LivenessAnalysis()
    {
        var ast = Parse();
        return _parser.GetSymbolTable().VariableCount;
    }
}