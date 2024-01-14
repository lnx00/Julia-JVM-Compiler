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

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}