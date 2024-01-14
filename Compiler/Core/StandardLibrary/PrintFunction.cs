using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.StandardLibrary;

public static class PrintFunction
{
    public static void Register(SymbolTable.SymbolTable symbolTable)
    {
        symbolTable.AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Integer) });
        symbolTable.AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Float64) });
        symbolTable.AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.String) });
        symbolTable.AddFunction("println", TypeManager.DataType.Void, new List<VariableSymbol> { new("value", TypeManager.DataType.Bool) });
    }

    public static List<Instruction> Translate(FunctionSymbol functionSymbol)
    {
        return new List<Instruction>
        {
            //new CallInstruction(CallInstruction.InvocationType.Virtual, functionSymbol)
            new CustomInstruction("getstatic", new List<string>{ "java/lang/System/out", "Ljava/io/PrintStream;" }),
            new CustomInstruction("invokevirtual", new List<string>{ "java/io/PrintStream/println(I)V" }),
        };
    }
}