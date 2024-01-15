using Compiler.Core.Common;

namespace Compiler.Core.SymbolTable.Symbols;

public class VariableSymbol : ISymbol
{
    public string Name { get; }
    public int Offset { get; set; }
    public TypeManager.DataType Type { get; }

    public VariableSymbol(string name, int offset, TypeManager.DataType type)
    {
        Name = name;
        Offset = offset;
        Type = type;
    }
}