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

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}