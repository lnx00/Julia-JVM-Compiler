using Compiler.CodeGenerator;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.StandardLibrary;

public interface IStlFunction
{
    public void Register(SymbolTable.SymbolTable symbolTable);
    public TranslationResult Translate(FunctionSymbol symbol, List<string> args);
}