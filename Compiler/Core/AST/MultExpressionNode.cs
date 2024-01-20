using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

namespace Compiler.Core.AST;

public class MultExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Mult,
        Div,
        Mod
    }
    
    public override TypeManager.DataType Type { get; }
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public MultExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op, TypeManager.DataType type)
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
            case Operation.Mult:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Mul, Type));
                break;
            
            case Operation.Div:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Div, Type));
                break;
            
            case Operation.Mod:
                instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Mod, Type));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TranslationResult(instructions, left.StackSize, right.StackSize);
    }
}