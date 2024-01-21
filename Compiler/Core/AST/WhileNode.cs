using Compiler.CodeGenerator;
using Compiler.Core.Common;
using Compiler.Core.IntermediateCode;

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
        List<Instruction> instructions = new()
        {
            new CommentInstruction("While loop")
        };
        int stackSize = 0;

        // Create labels
        var startLabel = LabelManager.GetLabel("whileStart");
        var endLabel = LabelManager.GetLabel("whileEnd");

        // Start label
        instructions.Add(startLabel);

        // Translate condition
        var condResult = Condition.Translate(ctx);
        instructions.AddRange(condResult.Instructions);
        stackSize = Math.Max(stackSize, condResult.StackSize);

        // Compare condition
        instructions.Add(new BranchInstruction(BranchInstruction.Condition.Equal, endLabel));

        // Translate body
        var bodyResult = Body.Translate(ctx);
        instructions.AddRange(bodyResult.Instructions);
        stackSize = Math.Max(stackSize, bodyResult.StackSize);

        // Jump to start
        instructions.Add(new BranchInstruction(BranchInstruction.Condition.None, startLabel));

        // End label
        instructions.Add(endLabel);

        return new TranslationResult(instructions, stackSize);
    }
}