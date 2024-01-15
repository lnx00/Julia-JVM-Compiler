using Compiler.Core.Common;
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

    public override List<string> Translate()
    {
        return new List<string> {$"\tiload {Symbol.Offset}"};
    }
}