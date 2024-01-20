using Compiler.CodeGenerator;

namespace Compiler.Core.AST;

public abstract class INode
{
    public abstract TranslationResult Translate(TranslationContext ctx);
}