using Compiler.Core.AST;

namespace Compiler.CodeGenerator;

public static class LivenessAnalyzer
{
    public static int Analyze(StartNode ast)
    {
        TranslationContext ctx = new("Liveness");
        return ast.LivenessAnalysis(ctx);
    }
}