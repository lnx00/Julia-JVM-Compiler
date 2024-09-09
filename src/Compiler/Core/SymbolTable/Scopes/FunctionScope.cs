using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable.Scopes;

public class FunctionScope
{
    private readonly Dictionary<string, List<FunctionSymbol>> _functions = new();
    
    public void AddFunction(string name, FunctionSymbol function)
    {
        if (!_functions.TryGetValue(name, out List<FunctionSymbol>? value))
        {
            value = new List<FunctionSymbol>();
            _functions.Add(name, value);
        }

        value.Add(function);
    }
    
    public List<FunctionSymbol>? GetFunction(string name)
    {
        return _functions.GetValueOrDefault(name);
    }
}