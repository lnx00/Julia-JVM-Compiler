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

    public override List<string> Translate()
    {
        throw new NotImplementedException();
    }
}