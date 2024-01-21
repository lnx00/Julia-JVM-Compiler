using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public static class LivenessAnalyzer
{
    public static void Analyze(TranslationResult result)
    {
        ControlFlowGraph cfg = ControlFlowGraph.Generate(result.Instructions);
        var inSet = new Dictionary<BasicBlock, HashSet<int>>();
        var outSet = new Dictionary<BasicBlock, HashSet<int>>();
        
        // Initialize in and out sets
        foreach (var block in cfg.Blocks)
        {
            inSet[block] = new HashSet<int>();
            outSet[block] = new HashSet<int>();
        }
        
        // Iterate until convergence
        bool changed = true;
        while (changed)
        {
            changed = false;
            
            foreach (var block in cfg.Blocks)
            {
                var newIn = new HashSet<int>();
                var newOut = new HashSet<int>();
                
                // out[B] = U in[S] for all successors S of B
                /*foreach (var successor in GetSuccessors(block, cfg))
                {
                    newOut.UnionWith(inSet[successor]);
                }*/
                
                // in[B] = use[B] U (out[B] - def[B])
                newIn.UnionWith(block.GetUses());
                newIn.UnionWith(newOut.Except(block.GetDefs()));
                
                // Check if in[B] or out[B] changed
                if (newIn.SetEquals(inSet[block]) && newOut.SetEquals(outSet[block])) { continue; }
                
                changed = true;
                inSet[block] = newIn;
                outSet[block] = newOut;
            }
        }
    }
}