//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/LNX/Documents/Rider/Coba-Projekt/Compiler/Parser/Grammar/Julia.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="JuliaParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IJuliaVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart([NotNull] JuliaParser.StartContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction([NotNull] JuliaParser.FunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameters([NotNull] JuliaParser.ParametersContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf([NotNull] JuliaParser.IfContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.while"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhile([NotNull] JuliaParser.WhileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] JuliaParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.body"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBody([NotNull] JuliaParser.BodyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] JuliaParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclaration([NotNull] JuliaParser.DeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] JuliaParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall([NotNull] JuliaParser.CallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.return"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturn([NotNull] JuliaParser.ReturnContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BoolExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolExpr([NotNull] JuliaParser.BoolExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstExpr([NotNull] JuliaParser.ConstExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MultExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultExpr([NotNull] JuliaParser.MultExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>VarExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarExpr([NotNull] JuliaParser.VarExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>CallExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallExpr([NotNull] JuliaParser.CallExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AddExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddExpr([NotNull] JuliaParser.AddExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>CompExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompExpr([NotNull] JuliaParser.CompExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NotExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotExpr([NotNull] JuliaParser.NotExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenExpr</c>
	/// labeled alternative in <see cref="JuliaParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenExpr([NotNull] JuliaParser.ParenExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.multOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultOp([NotNull] JuliaParser.MultOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.addOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddOp([NotNull] JuliaParser.AddOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.compOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompOp([NotNull] JuliaParser.CompOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.boolOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolOp([NotNull] JuliaParser.BoolOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.const"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConst([NotNull] JuliaParser.ConstContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.intValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntValue([NotNull] JuliaParser.IntValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.floatValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFloatValue([NotNull] JuliaParser.FloatValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="JuliaParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] JuliaParser.TypeContext context);
}
