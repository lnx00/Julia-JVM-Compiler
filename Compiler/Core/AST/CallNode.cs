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
        
        // Create args
        List<string> args = Arguments.SelectMany(arg => arg.Translate()).ToList();

        // Differentiate between STL and user functions
        var parameterTypes = Arguments.Aggregate(string.Empty, (current, arg) => current + TypeManager.GetJasminType(arg.Type));
        if (Symbol.StlFunction is not null)
        {
            List<string> translated = Symbol.StlFunction.Translate(Symbol, args);
            instructions.AddRange(translated);
        }
        else
        {
            // Push args
            instructions.AddRange(args);
            
            var returnType = TypeManager.GetJasminType(Type);
            instructions.Add($"\tinvokestatic Program/{Symbol.GetMangledName()}({parameterTypes}){returnType}");
        }

        return instructions;
    }
}