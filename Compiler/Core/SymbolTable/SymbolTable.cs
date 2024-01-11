using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<Dictionary<string, VariableSymbol>> _variableScopes = new();
    private readonly List<FunctionSymbol> _functions = new();
    private FunctionSymbol? _currentFunction = null;
    
    public SymbolTable()
    {
        EnterScope();
        
        // Built-in functions | TODO: Move this somewhere else
        var println = AddFunction("println", TypeManager.DataType.Void);
        println.Parameters.Add(new VariableSymbol("value", TypeManager.DataType.Any));
    }

    public void EnterScope()
    {
        _variableScopes.Push(new Dictionary<string, VariableSymbol>());
    }
    
    public void EnterFunctionScope(FunctionSymbol functionSymbol)
    {
        _currentFunction = functionSymbol;
        EnterScope();
    }

    public void LeaveScope()
    {
        _variableScopes.Pop();
    }
    
    public void LeaveFunctionScope()
    {
        _currentFunction = null;
        LeaveScope();
    }
    
    /*public void AddSymbol(string name, TypeManager.DataType type)
    {
        _scopes.Peek().Add(name, type);
    }*/
    
    public VariableSymbol AddVariable(string name, TypeManager.DataType type)
    {
        var symbol = new VariableSymbol(name, type);
        _variableScopes.Peek().Add(name, symbol);
        return symbol;
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type)
    {
        var symbol = new FunctionSymbol(name, type);
        _functions.Add(symbol);
        return symbol;
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type, List<VariableSymbol> parameters)
    {
        var symbol = new FunctionSymbol(name, type, parameters);
        _functions.Add(symbol);
        return symbol;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        foreach (Dictionary<string, VariableSymbol> scope in _variableScopes)
        {
            if (scope.TryGetValue(name, out var symbol))
            {
                return symbol;
            }
        }

        return null;
    }
    
    public FunctionSymbol? GetFunction(string name)
    {
        // TODO: Check for function overloading
        return _functions.FirstOrDefault(function => string.Equals(function.Name, name));
    }
    
    public FunctionSymbol? GetCurrentFunction()
    {
        return _currentFunction;
    }
    
    public bool IsVariable(string name)
    {
        return GetVariable(name) != null;
    }
    
    public bool IsFunction(string name)
    {
        return GetFunction(name) != null;
    }
    
    public bool IsDefined(string name)
    {
        return GetVariable(name) != null || GetFunction(name) != null;
    }
}