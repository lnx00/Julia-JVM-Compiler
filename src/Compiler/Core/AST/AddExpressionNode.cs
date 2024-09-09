using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

namespace Compiler.Core.AST;

public class AddExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Add,
        Subtract
    }
    
    public override TypeManager.DataType Type { get; }
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public AddExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op, TypeManager.DataType type)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        Type = type;
        OperationType = op;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<Instruction> instructions = new();

        var left = LeftExpression.Translate(ctx);
        var right = RightExpression.Translate(ctx);
        
        instructions.AddRange(left.Instructions);
        instructions.AddRange(right.Instructions);
        
        switch (OperationType)
        {
            case Operation.Add:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Add, Type));
                break;
            
            case Operation.Subtract:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Sub, Type));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TranslationResult(instructions, Math.Max(left.StackSize, right.StackSize) + 1);
    }
}