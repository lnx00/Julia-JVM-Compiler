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

    public override TranslationResult Translate(TranslationContext ctx)
    {
        return Symbol.Type switch
        {
            TypeManager.DataType.Integer => new TranslationResult(new List<string> { $"\tiload {Symbol.Offset}" }, 1),
            TypeManager.DataType.Bool => new TranslationResult(new List<string> { $"\tiload {Symbol.Offset}" }, 1),
            TypeManager.DataType.Float64 => new TranslationResult(new List<string> { $"\tfload {Symbol.Offset}" }, 1),
            TypeManager.DataType.String => new TranslationResult(new List<string> { $"\taload {Symbol.Offset}" }, 1),
            
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}