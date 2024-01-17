using Compiler.CodeGenerator;

namespace Compiler.Core.AST;

public abstract class INode
{
    public abstract List<string> Translate(TranslationContext ctx);
}