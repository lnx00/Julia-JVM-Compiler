using Compiler.Core.AST;

namespace Compiler.CodeGenerator;

public class CodeGenerator
{
    public List<string> Generate(BlockNode ast, string className)
    {
        List<string> instructions = new();
        TranslationContext ctx = new(className);
        
        // Header | TODO: Use source file name
        instructions.AddRange(new List<string>
        {
            ".bytecode 49.0",
            $".class public {className}",
            ".super java/lang/Object",
            string.Empty,
        });
        
        // Initializer
        instructions.AddRange(new List<string>
        {
            ".method public <init>()V",
            "\taload_0",
            "\tinvokespecial java/lang/Object/<init>()V",
            "\treturn",
            ".end method",
            string.Empty,
        });
        
        // Source code
        var result = ast.Translate(ctx);
        instructions.AddRange(result.Instructions);
        
        // Main method
        instructions.AddRange(new List<string>
        {
            ".method public static main([Ljava/lang/String;)V",
            "\t.limit stack 2",
            "\t.limit locals 1",
            $"\tinvokestatic {className}/_main()V",
            "\treturn",
            ".end method",
            string.Empty,
        });
        
        return instructions;
    }
}