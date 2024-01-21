using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

namespace Compiler.Core.AST;

public class CompExpressionNode : ExpressionNode
{
    public enum Operation
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    public override TypeManager.DataType Type { get; } = TypeManager.DataType.Bool;
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public CompExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        OperationType = op;
    }

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<Instruction> instructions = new();
        
        var left = LeftExpression.Translate(ctx);
        var right = RightExpression.Translate(ctx);

        instructions.AddRange(left.Instructions);
        instructions.AddRange(right.Instructions);
        instructions.Add(new ArithmeticInstruction(ArithmeticInstruction.Operation.Sub, LeftExpression.Type));
        
        var trueLabel = LabelManager.GetLabel("compTrue");
        var endLabel = LabelManager.GetLabel("compEnd");

        switch (OperationType)
        {
            case Operation.Equal:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.Equal, trueLabel));
                break;
            
            case Operation.NotEqual:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.NotEqual, trueLabel));
                break;
            
            case Operation.LessThan:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.LessThan, trueLabel));
                break;
            
            case Operation.LessThanOrEqual:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.LessThanOrEqual, trueLabel));
                break;
            
            case Operation.GreaterThan:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.GreaterThan, trueLabel));
                break;
            
            case Operation.GreaterThanOrEqual:
                instructions.Add(new BranchInstruction(BranchInstruction.Condition.GreaterThanOrEqual, trueLabel));
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        instructions.Add(new CustomInstruction("\ticonst_0"));
        instructions.Add(new BranchInstruction(BranchInstruction.Condition.None, endLabel));
        instructions.Add(trueLabel);
        instructions.Add(new CustomInstruction("\ticonst_1"));
        instructions.Add(endLabel);

        return new TranslationResult(instructions, left.StackSize, right.StackSize);
    }
}