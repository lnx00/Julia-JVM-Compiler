using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Scopes;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<VariableScope> _variableScopes = new();
    private readonly FunctionScope _functions = new();
    private FunctionSymbol? _currentFunction; // Hacky, but we only have one scope anyways...
    
    public SymbolTable()
    {
        // Enter initial global scope
        _variableScopes.Push(new VariableScope(0));
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
        var variableSymbol = new VariableSymbol(name, type);
        GetCurrentScope().AddVariable(name, variableSymbol);
        return variableSymbol;
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type, List<VariableSymbol> parameters)
    {
        var functionSymbol = new FunctionSymbol(name, type, parameters);
        _functions.AddFunction(name, functionSymbol);
        return functionSymbol;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        return _variableScopes.Select(scope => scope.GetVariable(name)).OfType<VariableSymbol>().FirstOrDefault();
    }
    
    public List<FunctionSymbol>? GetFunction(string name)
    {
        return _functions.GetFunction(name);
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