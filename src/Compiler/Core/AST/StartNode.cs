﻿using Compiler.CodeGenerator;
using Compiler.CodeGenerator.Liveness;
using Compiler.Core.IntermediateCode;

namespace Compiler.Core.AST;

public class StartNode : INode
{
    public List<INode> Statements { get; }

    public StartNode(List<INode> statements)
    {
        Statements = statements;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<Instruction> instructions = new();
        int stackSize = 0;

        foreach (var stmt in Statements)
        {
            if (stmt is not FunctionDefinitionNode funcDef) { continue; }
            
            var result = funcDef.Translate(ctx);
            instructions.AddRange(result.Instructions);
            stackSize = Math.Max(stackSize, result.StackSize);
        }
        
        return new TranslationResult(instructions, stackSize);
    }
    
    public int LivenessAnalysis(TranslationContext ctx)
    {
        int registers = 0;

        foreach (var stmt in Statements)
        {
            if (stmt is not FunctionDefinitionNode funcDef) { continue; }
            
            var result = funcDef.Translate(ctx);

            CFG cfg = new CFG(result.Instructions);
            int cfgResult = cfg.Analyze();
            registers = Math.Max(registers, cfgResult);
        }

        return registers;
    }
}