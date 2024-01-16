using Compiler.Core.Common;
using Compiler.Core.StandardLibrary;
using Compiler.Core.SymbolTable.Scopes;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<VariableScope> _variableScopes = new();
    private readonly FunctionScope _functions = new();
    private FunctionSymbol? _currentFunction; // Hacky, but we only have one scope anyways...
    
    private readonly PrintFunction _printFunction = new();
    
    public SymbolTable()
    {
        // Enter initial global scope
        _variableScopes.Push(new VariableScope(0));
        
        // Register standard library functions
        _printFunction.Register(this);
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
        var functionSymbol = new FunctionSymbol(name, type, parameters);
        _functions.AddFunction(name, functionSymbol);
        return functionSymbol;
    }
    
    public FunctionSymbol AddStlFunction(string name, TypeManager.DataType type, List<VariableSymbol> parameters, IStlFunction stlFunction)
    {
        var functionSymbol = AddFunction(name, type, parameters);
        functionSymbol.RegisterStlFunction(stlFunction);
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