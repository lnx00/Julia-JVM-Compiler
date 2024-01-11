using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

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
        var symbol = new VariableSymbol(name, type);
        _variables.Add(name, symbol);
        Offset++;
        return symbol;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        return _variables.GetValueOrDefault(name);
    }
}

public class SymbolTable
{
    private readonly Stack<VariableScope> _variableScopes = new();
    private readonly Dictionary<string, List<FunctionSymbol>> _functions = new();
    private FunctionSymbol? _currentFunction;
    
    public SymbolTable()
    {
        // Enter global scope
        _variableScopes.Push(new VariableScope(0));
        
        // Built-in functions | TODO: Move this somewhere else
        AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Integer) });
        AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Float64) });
        AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.String) });
        AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Bool) });
    }

    private VariableScope GetCurrentScope()
    {
        return _variableScopes.Peek();
    }
    
    public void EnterScope()
    {
        _variableScopes.Push(new VariableScope(GetCurrentScope().Offset));
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
        return GetCurrentScope().AddVariable(name, type);
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
        return _variableScopes.Select(scope => scope.GetVariable(name)).OfType<VariableSymbol>().FirstOrDefault();
    }
    
    public List<FunctionSymbol>? GetFunction(string name)
    {
        return _functions.GetValueOrDefault(name);
    }
    
    public FunctionSymbol? GetCurrentFunction()
    {
        return _currentFunction;
    }
    
    public bool IsDefined(string name)
    {
        return GetVariable(name) != null || GetFunction(name) != null;
    }
}