using Compiler.Core.AST;
using Compiler.Core.IntermediateCode;
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
    
    private List<Instruction> GenerateCode(BlockNode ast)
    {
        return _codeGenerator.Generate(ast);
    }
    
    public void Compile()
    {
        try
        {
            var ast = Parse();
            var code = GenerateCode(ast);
        }
        catch (ParserException e)
        {
            Console.Error.WriteLine($"Error at line {e.Line}, position {e.Position}: {e.Message}");
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Unknown parser error: {e.Message}");
        }
    }
    
    public void LivenessAnalysis()
    {
        Console.Error.WriteLine("Not implemented yet");
    }
}