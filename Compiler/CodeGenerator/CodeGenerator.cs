using Compiler.Core.AST;
using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator;

public class CodeGenerator
{
    public List<string> Generate(BlockNode ast)
    {
        return ast.Translate();
    }
}