using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class CallNode : ExpressionNode
{
    public FunctionSymbol Symbol { get; set; }
    public override TypeManager.DataType Type { get; }
    public List<ExpressionNode> Arguments { get; }
    
    public CallNode(FunctionSymbol symbol, List<ExpressionNode> arguments, TypeManager.DataType type)
    {
        Symbol = symbol;
        Arguments = arguments;
        Type = type;
    }
}