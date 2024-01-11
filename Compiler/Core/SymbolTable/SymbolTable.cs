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
        AddFunction("println", TypeManager.DataType.Void).Parameters.Add(new VariableSymbol("value", TypeManager.DataType.Integer));
        AddFunction("println", TypeManager.DataType.Void).Parameters.Add(new VariableSymbol("value", TypeManager.DataType.Float64));
        AddFunction("println", TypeManager.DataType.Void).Parameters.Add(new VariableSymbol("value", TypeManager.DataType.Bool));
        AddFunction("println", TypeManager.DataType.Void).Parameters.Add(new VariableSymbol("value", TypeManager.DataType.String));
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
        if (!_functions.TryGetValue(name, out List<FunctionSymbol>? value))
        {
            value = new List<FunctionSymbol>();
            _functions.Add(name, value);
        }
        
        var symbol = new FunctionSymbol(name, type, parameters);
        value.Add(symbol);
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
    
    public List<FunctionSymbol>? GetFunction(string name)
    {
        return _functions.GetValueOrDefault(name);
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