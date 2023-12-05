using Compiler.Core.Common;

namespace Compiler.Core.SymbolTable.Symbols;

public class FunctionSymbol : ISymbol
{
    public string Name { get; }
    public TypeManager.DataType? Type { get; }

    public FunctionSymbol(string name, TypeManager.DataType? type)
    {
        Name = name;
        Type = type;
    }
}