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

    public override List<string> Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        
        instructions.AddRange(Expression.Translate(ctx));
        switch (OperationType)
        {
            case Operation.Not:
                instructions.Add("\tldc 1");
                instructions.Add("\tixor");
                break;
            
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
        
        return instructions;
    }
}