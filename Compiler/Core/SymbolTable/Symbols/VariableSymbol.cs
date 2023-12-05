using Compiler.Core.Common;

namespace Compiler.Core.SymbolTable.Symbols;

public class VariableSymbol : ISymbol
{
public string Name { get; }
    public TypeManager.DataType Type { get; }

    public VariableSymbol(string name, TypeManager.DataType type)
    {
        Name = name;
        Type = type;
    }
}