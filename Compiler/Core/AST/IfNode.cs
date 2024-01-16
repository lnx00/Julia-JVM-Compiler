using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class IfNode : INode
{
    public ExpressionNode Condition { get; }
    public BlockNode Body { get; }
    public BlockNode? ElseBody { get; }

    public IfNode(ExpressionNode condition, BlockNode body, BlockNode? elseBody)
    {
        Condition = condition;
        Body = body;
        ElseBody = elseBody;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new() { "\n\t; If statement" };

        // Translate condition
        instructions.AddRange(Condition.Translate());

        // Create labels
        string elseLabel = LabelManager.GetLabel("else");
        string endLabel = LabelManager.GetLabel("end");

        // Compare condition to 0
        instructions.Add("\tifeq " + elseLabel);

        // Translate body
        instructions.AddRange(Body.Translate());

        // Jump to end
        instructions.Add("\tgoto " + endLabel);

        // Else label
        instructions.Add(elseLabel + ":");

        // Translate else body
        if (ElseBody is not null)
        {
            instructions.AddRange(ElseBody.Translate());
        }

        // End label
        instructions.Add(endLabel + ":");

        return instructions;
    }
}
