using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable.Scopes;

public class VariableScope
{
    public int Offset { get; private set; }
    private readonly Dictionary<string, VariableSymbol> _variables = new();

    public VariableScope(int offset)
    {
        Offset = offset;
    }

    public VariableSymbol AddVariable(string name, TypeManager.DataType type)
    {
        var symbol = new VariableSymbol(name, Offset++, type);
        _variables.Add(name, symbol);

        return symbol;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        return _variables.GetValueOrDefault(name);
    }
}