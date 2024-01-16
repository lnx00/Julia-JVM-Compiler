using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class DeclarationNode : INode
{
    public VariableSymbol Symbol { get; }
    public ExpressionNode Value { get; }

    public DeclarationNode(VariableSymbol symbol, ExpressionNode value)
    {
        Symbol = symbol;
        Value = value;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new();

        instructions.AddRange(Value.Translate());
        instructions.Add($"\tistore {Symbol.Offset}");

        return instructions;
    }
}