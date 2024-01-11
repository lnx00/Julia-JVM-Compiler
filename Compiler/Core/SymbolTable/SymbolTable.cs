using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<Dictionary<string, VariableSymbol>> _variableScopes = new();
    private readonly Dictionary<string, List<FunctionSymbol>> _functions = new();
    private FunctionSymbol? _currentFunction;
    
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
    
    public VariableSymbol AddVariable(string name, TypeManager.DataType type)
    {
        var symbol = new VariableSymbol(name, type);
        _variableScopes.Peek().Add(name, symbol);
        return symbol;
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type)
    {
        return AddFunction(name, type, new List<VariableSymbol>());
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type, List<VariableSymbol> parameters)
    {
        if (!_functions.ContainsKey(name))
        {
            _functions.Add(name, new List<FunctionSymbol>());
        }
        
        var symbol = new FunctionSymbol(name, type, parameters);
        _functions[name].Add(symbol);
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
        //return _functions.FirstOrDefault(function => string.Equals(function.Name, name));
        if (_functions.TryGetValue(name, out var functions))
        {
            return functions.First();
        }
        
        return null;
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