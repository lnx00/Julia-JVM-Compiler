using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.SymbolTable;

public class SymbolTable
{
    private readonly Stack<Dictionary<string, ISymbol>> _scopes = new();
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
        _scopes.Push(new Dictionary<string, ISymbol>());
    }
    
    public void EnterFunctionScope(FunctionSymbol functionSymbol)
    {
        _currentFunction = functionSymbol;
        EnterScope();
    }

    public void LeaveScope()
    {
        _scopes.Pop();
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
        _scopes.Peek().Add(name, symbol);
        return symbol;
    }
    
    public FunctionSymbol AddFunction(string name, TypeManager.DataType type)
    {
        var symbol = new FunctionSymbol(name, type);
        _scopes.Peek().Add(name, symbol);
        return symbol;
    }
    
    private ISymbol? GetSymbol(string name)
    {
        foreach (Dictionary<string, ISymbol> scope in _scopes)
        {
            if (scope.TryGetValue(name, out var symbol))
            {
                return symbol;
            }
        }

        return null;
    }
    
    public VariableSymbol? GetVariable(string name)
    {
        ISymbol? symbol = GetSymbol(name);
        return symbol is VariableSymbol variableSymbol ? variableSymbol : null;
    }
    
    public FunctionSymbol? GetFunction(string name)
    {
        ISymbol? symbol = GetSymbol(name);
        return symbol is FunctionSymbol functionSymbol ? functionSymbol : null;
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
        return GetSymbol(name) != null;
    }
}