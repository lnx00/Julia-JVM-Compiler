using Compiler.CodeGenerator;
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

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new() { "\n\t; While statement" };
        int stackSize = 0;

        // Create labels
        string startLabel = LabelManager.GetLabel("whileStart");
        string endLabel = LabelManager.GetLabel("whileEnd");

        // Start label
        instructions.Add(startLabel + ":");

        // Translate condition
        var condResult = Condition.Translate(ctx);
        instructions.AddRange(condResult.Instructions);
        stackSize = Math.Max(stackSize, condResult.StackSize);

        // Compare condition
        instructions.Add("\tifeq " + endLabel);

        // Translate body
        var bodyResult = Body.Translate(ctx);
        instructions.AddRange(bodyResult.Instructions);
        stackSize = Math.Max(stackSize, bodyResult.StackSize);

        // Jump to start
        instructions.Add("\tgoto " + startLabel);

        // End label
        instructions.Add(endLabel + ":");

        return new TranslationResult(instructions, stackSize);
    }
}