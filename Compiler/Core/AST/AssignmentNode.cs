﻿using Compiler.CodeGenerator;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class AssignmentNode : INode
{
    public VariableSymbol Symbol { get; }
    public ExpressionNode Value { get; }

    public AssignmentNode(VariableSymbol symbol, ExpressionNode value)
    {
        Symbol = symbol;
        Value = value;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        
        var result = Value.Translate(ctx);

        instructions.AddRange(result.Instructions);
        instructions.Add($"\tistore {Symbol.Offset}");

        return new TranslationResult(instructions, result.StackSize);
    }
}