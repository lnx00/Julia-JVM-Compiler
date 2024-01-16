using Compiler.Core.Common;

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

    public override List<string> Translate()
    {
        List<string> instructions = new();

        instructions.AddRange(LeftExpression.Translate());
        instructions.AddRange(RightExpression.Translate());
        
        string trueLabel = LabelManager.GetLabel("compTrue");
        string endLabel = LabelManager.GetLabel("compEnd");

        switch (OperationType)
        {
            case Operation.Equal:
                instructions.Add("\tif_icmpeq " + trueLabel);
                break;
            
            case Operation.NotEqual:
                instructions.Add("\tif_icmpne " + trueLabel);
                break;
            
            case Operation.LessThan:
                instructions.Add("\tif_icmplt " + trueLabel);
                break;
            
            case Operation.LessThanOrEqual:
                instructions.Add("\tif_icmple " + trueLabel);
                break;
            
            case Operation.GreaterThan:
                instructions.Add("\tif_icmpgt " + trueLabel);
                break;
            
            case Operation.GreaterThanOrEqual:
                instructions.Add("\tif_icmpge " + trueLabel);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        instructions.Add("\ticonst_0");
        instructions.Add("\tgoto " + endLabel);
        instructions.Add(trueLabel + ":");
        instructions.Add("\ticonst_1");
        instructions.Add(endLabel + ":");

        return instructions;
    }
}