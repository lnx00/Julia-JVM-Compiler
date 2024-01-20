using Compiler.CodeGenerator;
using Compiler.Core.Common;

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
        List<string> instructions = new();

        var left = LeftExpression.Translate(ctx);
        var right = RightExpression.Translate(ctx);
        
        instructions.AddRange(left.Instructions);
        instructions.AddRange(right.Instructions);
        
        switch (OperationType)
        {
            case Operation.Add:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\tiadd");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfadd");
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            case Operation.Subtract:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\tisub");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfsub");
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TranslationResult(instructions, left.StackSize, right.StackSize);
    }
}