using System.Text;
using Compiler.Core.Common;
using Compiler.Core.StandardLibrary;

namespace Compiler.Core.SymbolTable.Symbols;

public class FunctionSymbol : ISymbol
{
    public string Name { get; }
    public TypeManager.DataType Type { get; }
    public List<VariableSymbol> Parameters { get; }
    public IStlFunction? StlFunction { get; private set; }
    
    public FunctionSymbol(string name, TypeManager.DataType type, List<VariableSymbol> parameters)
    {
        Name = name;
        Type = type;
        Parameters = parameters;
    }
    
    public void RegisterStlFunction(IStlFunction stlFunction)
    {
        StlFunction = stlFunction;
    }
    
    public string GetMangledName()
    {
        return $"_{Name}";
    }
}