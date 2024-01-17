using Compiler.CodeGenerator;
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

    public override List<string> Translate(TranslationContext ctx)
    {
        return Symbol.Type switch
        {
            TypeManager.DataType.Integer => new List<string> { $"\tiload {Symbol.Offset}" },
            TypeManager.DataType.Bool => new List<string> { $"\tiload {Symbol.Offset}" },
            TypeManager.DataType.Float64 => new List<string> { $"\tfload {Symbol.Offset}" },
            TypeManager.DataType.String => new List<string> { $"\taload {Symbol.Offset}" },
            
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}