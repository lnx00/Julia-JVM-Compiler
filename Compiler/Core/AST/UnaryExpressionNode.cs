using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class UnaryExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Not,
        Negate
    }
    
    public override TypeManager.DataType Type { get; }
    public ExpressionNode Expression { get; }
    public Operation OperationType { get; }
    
    public UnaryExpressionNode(ExpressionNode expression, Operation op)
    {
        Expression = expression;
        Type = expression.Type;
        OperationType = op;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        
        var result = Expression.Translate(ctx);
        instructions.AddRange(result.Instructions);
        
        switch (OperationType)
        {
            case Operation.Not:
                instructions.Add("\tldc 1");
                instructions.Add("\tixor");
                return new TranslationResult(instructions, result.StackSize + 1);
            
            case Operation.Negate:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\tineg");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfneg");
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return new TranslationResult(instructions, result.StackSize);
    }
}