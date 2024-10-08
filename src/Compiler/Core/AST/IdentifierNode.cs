﻿using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class IdentifierNode : ExpressionNode
{
    public VariableSymbol Symbol { get; }
    public override TypeManager.DataType Type { get; }
    
    public IdentifierNode(VariableSymbol symbol, TypeManager.DataType type)
    {
        Symbol = symbol;
        Type = type;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        return new TranslationResult(new LoadInstruction(Symbol.Offset, Type), 1);
    }
}