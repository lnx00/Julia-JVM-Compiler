using Compiler.Core.IntermediateCode;

namespace Compiler.CodeGenerator.Liveness;

public class BasicBlock
{
    public List<Instruction> Instructions { get; }
    public List<BasicBlock> Successors { get; } = new();
    
    public BasicBlock(List<Instruction> instructions)
    {
        Instructions = instructions;
    }
    
    public HashSet<int> GetDefs()
    {
        HashSet<int> defs = new();
        Dictionary<int, bool> used = new();
        
        foreach (var instruction in Instructions)
        {
            if (instruction is LoadInstruction load)
            {
                used[load.Offset] = true;
            }
            
            if (instruction is StoreInstruction store)
            {
                if (used.ContainsKey(store.Offset)) { continue; }
                defs.Add(store.Offset);
            }
        }
        
        return defs;
    }
    
    public HashSet<int> GetUses()
    {
        HashSet<int> uses = new();
        Dictionary<int, bool> defined = new();
        
        foreach (var instruction in Instructions)
        {
            if (instruction is StoreInstruction store)
            {
                defined[store.Offset] = true;
            }
            
            if (instruction is LoadInstruction load)
            {
                if (defined.ContainsKey(load.Offset)) { continue; }
                uses.Add(load.Offset);
            }
        }
        
        return uses;
    }
}