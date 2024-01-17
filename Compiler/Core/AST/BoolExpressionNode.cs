using Compiler.CodeGenerator;
using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class BoolExpressionNode : ExpressionNode
{
    public enum Operation
    {
        And,
        Or,
    }
    
    public override TypeManager.DataType Type { get; }
    public ExpressionNode LeftExpression { get; }
    public ExpressionNode RightExpression { get; }
    public Operation OperationType { get; }

    public BoolExpressionNode(ExpressionNode leftExpression, ExpressionNode rightExpression, Operation op, TypeManager.DataType type)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
        Type = type;
        OperationType = op;
    }

    public override List<string> Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        
        instructions.AddRange(LeftExpression.Translate(ctx));
        instructions.AddRange(RightExpression.Translate(ctx));

        switch (OperationType)
        {
            case Operation.And:
                instructions.Add("\tiand");
                break;
            
            case Operation.Or:
                instructions.Add("\tior");
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return instructions;
    }
}