using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.StandardLibrary;

public class PrintFunction : IStlFunction
{
    public void Register(SymbolTable.SymbolTable symbolTable)
    {
        symbolTable.AddStlFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", 0, TypeManager.DataType.Integer) }, this);
        symbolTable.AddStlFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", 0, TypeManager.DataType.Float64) }, this);
        symbolTable.AddStlFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", 0, TypeManager.DataType.String) }, this);
        symbolTable.AddStlFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", 0, TypeManager.DataType.Bool) }, this);
    }

    public TranslationResult Translate(FunctionSymbol symbol, List<Instruction> args)
    {
        List<Instruction> instructions = new();
        var argTypes = symbol.Parameters.Aggregate(string.Empty, (current, arg) => current + TypeManager.GetJasminType(arg.Type));
        
        instructions.Add(new CustomInstruction("\tgetstatic java/lang/System/out Ljava/io/PrintStream;"));
        instructions.AddRange(args);
        instructions.Add(new InvokeInstruction($"java/io/PrintStream/println({argTypes})V", InvokeInstruction.InvokeType.Virtual));
        
        return new TranslationResult(instructions, 1);
    }
}