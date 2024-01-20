using Compiler.CodeGenerator;
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

    public override TranslationResult Translate(TranslationContext ctx)
    {
        List<string> instructions = new() { "\n\t; If statement" };
        int stackSize = 0;

        // Translate condition
        var condResult = Condition.Translate(ctx);
        instructions.AddRange(condResult.Instructions);
        stackSize = Math.Max(stackSize, condResult.StackSize);

        // Create labels
        string elseLabel = LabelManager.GetLabel("else");
        string endLabel = LabelManager.GetLabel("end");

        // Compare condition
        instructions.Add("\tifeq " + elseLabel);

        // Translate body
        var bodyResult = Body.Translate(ctx);
        instructions.AddRange(bodyResult.Instructions);
        stackSize = Math.Max(stackSize, bodyResult.StackSize);

        // Jump to end
        instructions.Add("\tgoto " + endLabel);

        // Else label
        instructions.Add(elseLabel + ":");

        // Translate else body
        if (ElseBody is not null)
        {
            var elseResult = ElseBody.Translate(ctx);
            instructions.AddRange(elseResult.Instructions);
            stackSize = Math.Max(stackSize, elseResult.StackSize);
        }

        // End label
        instructions.Add(endLabel + ":");

        return new TranslationResult(instructions, stackSize);
    }
}
