using Compiler.Core.Common;

namespace Compiler.Core.SymbolTable.Symbols;

public class FunctionSymbol : ISymbol
{
    public string Name { get; }
    public TypeManager.DataType Type { get; }
    public List<VariableSymbol> Parameters { get; }
    
    public FunctionSymbol(string name, TypeManager.DataType type, List<VariableSymbol> parameters)
    {
        Name = name;
        Type = type;
        Parameters = parameters;
    }
}