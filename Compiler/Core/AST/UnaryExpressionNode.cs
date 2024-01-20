using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

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
        List<Instruction> instructions = new();
        
        var result = Expression.Translate(ctx);
        instructions.AddRange(result.Instructions);
        
        switch (OperationType)
        {
            case Operation.Not:
                instructions.Add(new ConstInstruction(1));
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Xor, Type));
                return new TranslationResult(instructions, result.StackSize + 1);
            
            case Operation.Negate:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Neg, Type));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return new TranslationResult(instructions, result.StackSize);
    }
}