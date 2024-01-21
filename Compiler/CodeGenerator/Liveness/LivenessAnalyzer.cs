namespace Compiler.CodeGenerator.Liveness;

public static class LivenessAnalyzer
{
    public static void Analyze(TranslationResult result)
    {
        ControlFlowGraph cfg = ControlFlowGraph.Generate(result.Instructions);
        // TODO
    }
}