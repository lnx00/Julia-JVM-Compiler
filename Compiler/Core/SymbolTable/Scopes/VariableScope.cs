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

    public void AddVariable(string name, VariableSymbol variable)
    {
        _variables.Add(name, variable);
        Offset++;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        return _variables.GetValueOrDefault(name);
    }
}