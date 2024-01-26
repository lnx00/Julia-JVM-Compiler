using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class CFG
{
    public List<BasicBlock> Blocks { get; } = new();
    
    public BasicBlock Entry { get; } = new("@entry");
    public BasicBlock Exit { get; } = new("@exit");

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
        Blocks.Add(Exit);
        
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
        // Initialize the Defs and Uses for each block
        foreach (var block in Blocks)
        {
            block.Analyze();
        }
        
        while (true)
        {
            bool hasChanged = false;
            
            for (int i = Blocks.Count - 1; i >= 0; i--)
            {
                var block = Blocks[i];
                
                HashSet<int> liveOutBackup = new(block.LiveOut);
                HashSet<int> liveInBackup = new(block.LiveIn);
                
                // Update liveOut
                foreach (var successor in block.Successors)
                {
                    block.LiveOut.UnionWith(successor.LiveIn);
                }
                
                // Update liveIn
                HashSet<int> liveOutWithoutDefs = new(block.LiveOut);
                liveOutWithoutDefs.ExceptWith(block.Defs);
                block.LiveIn.UnionWith(block.Uses);
                block.LiveIn.UnionWith(liveOutWithoutDefs);
                
                // Check if liveIn or liveOut changed
                if (!liveOutBackup.SetEquals(block.LiveOut) || !liveInBackup.SetEquals(block.LiveIn))
                {
                    hasChanged = true;
                }
            }
            
            if (!hasChanged) { break; }
        }
        
        // Calculate max amount of live variables
        return Blocks.Select(block => block.LiveIn.Count).Max();
    }
}