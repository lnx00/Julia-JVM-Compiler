using Compiler.Core.Common;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<Dictionary<string, TypeManager.DataType>> _scopes = new();
    
    public SymbolTable()
    {
        EnterScope();
    }

    public void EnterScope()
    {
        _scopes.Push(new Dictionary<string, TypeManager.DataType>());
    }

    public void LeaveScope()
    {
        _scopes.Pop();
    }
    
    public void AddSymbol(string name, TypeManager.DataType type)
    {
        _scopes.Peek().Add(name, type);
    }
    
    public TypeManager.DataType? GetSymbol(string name)
    {
        foreach (Dictionary<string, TypeManager.DataType> scope in _scopes)
        {
            if (scope.TryGetValue(name, out var symbol))
            {
                return symbol;
            }
        }

        return null;
    }
    
    public bool IsDefined(string name)
    {
        return GetSymbol(name) != null;
    }
}