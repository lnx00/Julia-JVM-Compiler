using Compiler.Core.Common;

namespace Compiler.Core.AST;

public class WhileNode : INode
{
    public ExpressionNode Condition { get; }
    public BlockNode Body { get; }

    public WhileNode(ExpressionNode condition, BlockNode body)
    {
        Condition = condition;
        Body = body;
    }

    public override List<string> Translate()
    {
        List<string> instructions = new() { "\n\t; While statement" };

        // Create labels
        string startLabel = LabelManager.GetLabel("whileStart");
        string endLabel = LabelManager.GetLabel("whileEnd");

        // Start label
        instructions.Add(startLabel + ":");

        // Translate condition
        instructions.AddRange(Condition.Translate());

        // Compare condition to 0
        instructions.Add("\tifeq " + endLabel);

        // Translate body
        instructions.AddRange(Body.Translate());

        // Jump to start
        instructions.Add("\tgoto " + startLabel);

        // End label
        instructions.Add(endLabel + ":");

        return instructions;
    }
}