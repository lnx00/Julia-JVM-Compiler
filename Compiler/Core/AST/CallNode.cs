using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class CallNode : ExpressionNode
{
    public FunctionSymbol Symbol { get; set; }
    public override TypeManager.DataType Type { get; }
    public List<ExpressionNode> Arguments { get; }
    
    public CallNode(FunctionSymbol symbol, List<ExpressionNode> arguments, TypeManager.DataType type)
    {
        Symbol = symbol;
        Arguments = arguments;
        Type = type;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new();
        
        // Push arguments to stack
        foreach (var argument in Arguments)
        {
            instructions.AddRange(argument.Translate());
        }

        var parameterTypes = Arguments.Aggregate(string.Empty, (current, arg) => current + TypeManager.GetJasminType(arg.Type));
        var returnType = TypeManager.GetJasminType(Type);
        instructions.Add($"\tinvokestatic Program/{Symbol.GetMangledName()}({parameterTypes}){returnType}");

        return instructions;
    }
}