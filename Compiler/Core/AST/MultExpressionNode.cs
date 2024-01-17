using Compiler.CodeGenerator;
using Compiler.Core.Common;

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

    public override List<string> Translate(TranslationContext ctx)
    {
        List<string> instructions = new();
        
        instructions.AddRange(LeftExpression.Translate(ctx));
        instructions.AddRange(RightExpression.Translate(ctx));

        switch (OperationType)
        {
            case Operation.Mult:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\timul");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfmul");
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            case Operation.Div:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\tidiv");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfdiv");
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            
            case Operation.Mod:
                switch (Type)
                {
                    case TypeManager.DataType.Integer:
                        instructions.Add("\tirem");
                        break;
                    
                    case TypeManager.DataType.Float64:
                        instructions.Add("\tfrem");
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