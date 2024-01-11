using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class FunctionDefinitionNode : INode
{
    public FunctionSymbol Symbol { get; }
    public BlockNode Block { get; }
    public ParameterNode Parameters { get; }

    public FunctionDefinitionNode(FunctionSymbol symbol, BlockNode block, ParameterNode parameters)
    {
        Symbol = symbol;
        Block = block;
        Parameters = parameters;
    }
}