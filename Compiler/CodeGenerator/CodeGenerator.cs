using Compiler.Core.AST;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator;

public class CodeGenerator
{
    public List<Instruction> Generate(StartNode ast, string className)
    {
        List<Instruction> instructions = new();
        TranslationContext ctx = new(className);
        
        // Header | TODO: Use source file name
        instructions.AddRange(new List<Instruction>
        {
            new CustomInstruction(".bytecode 49.0"),
            new CustomInstruction($".class public {className}"),
            new CustomInstruction(".super java/lang/Object"),
            new SeparatorInstruction(),
        });
        
        // Initializer
        instructions.AddRange(new List<Instruction>
        {
            new CustomInstruction(".method public <init>()V"),
            new CustomInstruction("\taload_0"),
            new InvokeInstruction("java/lang/Object/<init>()V", InvokeInstruction.InvokeType.Special),
            new ReturnInstruction(TypeManager.DataType.Void),
            new CustomInstruction(".end method"),
            new SeparatorInstruction(),
        });
        
        // Source code
        var result = ast.Translate(ctx);
        instructions.AddRange(result.Instructions);
        
        // Main method
        instructions.AddRange(new List<Instruction>
        {
            new CustomInstruction(".method public static main([Ljava/lang/String;)V"),
            new CustomInstruction("\t.limit stack 2"),
            new CustomInstruction("\t.limit locals 1"),
            new InvokeInstruction($"{className}/_main()V", InvokeInstruction.InvokeType.Static),
            new ReturnInstruction(TypeManager.DataType.Void),
            new CustomInstruction(".end method"),
            new SeparatorInstruction(),
        });
        
        return instructions;
    }
}