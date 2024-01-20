using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.SymbolTable.Symbols;

namespace Compiler.Core.AST;

public class CallNode : ExpressionNode
{
    public FunctionSymbol Symbol { get; }
    public override TypeManager.DataType Type { get; }
    public List<ExpressionNode> Arguments { get; }
    
    public CallNode(FunctionSymbol symbol, List<ExpressionNode> arguments, TypeManager.DataType type)
    {
        Symbol = symbol;
        Arguments = arguments;
        Type = type;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        int stackSize = 0;
        
        // Create args
        //List<string> args = Arguments.SelectMany(arg => arg.Translate(ctx)).ToList();
        List<string> args = new();
        foreach (var result in Arguments.Select(arg => arg.Translate(ctx)))
        {
            args.AddRange(result.Instructions);
            stackSize += result.StackSize;
        }

        // Differentiate between STL and user functions
        var parameterTypes = Arguments.Aggregate(string.Empty, (current, arg) => current + TypeManager.GetJasminType(arg.Type));
        if (Symbol.StlFunction is not null)
        {
            var result = Symbol.StlFunction.Translate(Symbol, args);
            instructions.AddRange(result.Instructions);
            stackSize += result.StackSize;
        }
        else
        {
            // Push args
            instructions.AddRange(args);
            
            var returnType = TypeManager.GetJasminType(Type);
            instructions.Add($"\tinvokestatic {ctx.Name}/{Symbol.GetMangledName()}({parameterTypes}){returnType}");
        }

        return new TranslationResult(instructions, stackSize);
    }
}