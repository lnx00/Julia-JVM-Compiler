using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class CFG
{
    public List<BasicBlock> Blocks { get; } = new();
    
    public BasicBlock Entry { get; } = new();
    public BasicBlock Exit { get; } = new();

    public CFG(List<Instruction> instructions)
    {
        // Entry block
        BasicBlock currentBlock = Entry;
        Blocks.Add(currentBlock);
        
        // Split blocks on labels and branches
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case LabelInstruction label:
                {
                    var lastBlock = currentBlock;
                    
                    currentBlock = new BasicBlock(currentBlock)
                    {
                        Label = label.Label
                    };
                    currentBlock.Instructions.Add(instruction);
                    Blocks.Add(currentBlock);
                    
                    lastBlock.Successors.Add(currentBlock);
                    break;
                }

                case BranchInstruction branch:
                {
                    var lastBlock = currentBlock;
                    
                    currentBlock.Instructions.Add(instruction);
                    currentBlock.Target = branch.Label.Label;
                    
                    currentBlock = new BasicBlock(currentBlock);
                    Blocks.Add(currentBlock);

                    // Special case for goto
                    if (!branch.IsUnconditional)
                    {
                        lastBlock.Successors.Add(currentBlock);
                    }
                    break;
                }
                
                case ReturnInstruction:
                {
                    var lastBlock = currentBlock;
                    currentBlock.Instructions.Add(instruction);
                    
                    currentBlock = new BasicBlock(currentBlock);
                    Blocks.Add(currentBlock);
                    
                    lastBlock.Successors.Add(Exit);
                    Exit.Predecessors.Add(lastBlock);
                    break;
                }
                
                case CommentInstruction:
                    break;
                
                default:
                    currentBlock.Instructions.Add(instruction);
                    break;
            }
        }
        
        // Exit block
        currentBlock.Successors.Add(Exit);
        Exit.Predecessors.Add(currentBlock);
        
        // Link blocks
        foreach (var block in Blocks)
        {
            if (block.Target is not null)
            {
                var targetBlock = GetBlockByLabel(block.Target);
                if (targetBlock is not null)
                {
                    block.Successors.Add(targetBlock);
                    targetBlock.Predecessors.Add(block);
                }
            }
        }
    }
    
    public BasicBlock? GetBlockByLabel(string label)
    {
        return Blocks.FirstOrDefault(b => b.Label == label);
    }

    public int Analyze()
    {
        foreach (var block in Blocks)
        {
            block.Analyze();
        }
        
        bool changed = true;
        while (changed)
        {
            changed = false;
            
            foreach (var block in Blocks)
            {
                // Backup liveIn and liveOut
                var liveInBackup = new HashSet<int>(block.LiveIn);
                var liveOutBackup = new HashSet<int>(block.LiveOut);
                
                // liveOut = union of successors' liveIn
                foreach (var successor in block.Successors)
                {
                    block.LiveOut.UnionWith(successor.LiveIn);
                }
                
                // liveIn = uses union (liveOut - defs)
                var liveOutMinusDefs = new HashSet<int>(block.LiveOut);
                liveOutMinusDefs.ExceptWith(block.Defs);
                block.LiveIn.UnionWith(block.Uses);
                block.LiveIn.UnionWith(liveOutMinusDefs);
                
                // Check if liveIn or liveOut changed
                if (!block.LiveIn.SetEquals(liveInBackup) || !block.LiveOut.SetEquals(liveOutBackup))
                {
                    changed = true;
                }
            }
        }
        
        // Cound number of variables
        var variables = new HashSet<int>();
        foreach (var block in Blocks)
        {
            variables.UnionWith(block.LiveIn);
            variables.UnionWith(block.LiveOut);
        }
        
        return variables.Count;
    }
}