using Compiler.Core.Common;

namespace Compiler.Parser.Visitor;

public class JuliaVisitor : JuliaBaseVisitor<object?>
{
    public override object? VisitDeclaration(JuliaParser.DeclarationContext context)
    {
        string varName = context.IDENTIFIER().GetText();
        var typeName = Visit(context.type()) as TypeManager.DataType? ?? throw new Exception("Unknown type");
        var value = Visit(context.expression());

        //return base.VisitDeclaration(context);
        return null;
    }

    public override object? VisitAssignment(JuliaParser.AssignmentContext context)
    {
        string varName = context.IDENTIFIER().GetText();
        object? value = Visit(context.expression());
        
        //return base.VisitAssignment(context);
        return null;
    }

    public override object? VisitConst(JuliaParser.ConstContext context)
    {
        if (context.INTCONST() != null)
        {
            return int.Parse(context.INTCONST().GetText());
        }
        
        if (context.FLTCONST() != null)
        {
            return float.Parse(context.FLTCONST().GetText());
        }
        
        if (context.STRCONST() != null)
        {
            return context.STRCONST().GetText();
        }
        
        if (context.BOOLCONST() != null)
        {
            return bool.Parse(context.BOOLCONST().GetText());
        }
        
        //return base.VisitConst(context);
        throw new Exception("Unknown constant type");
    }

    public override object? VisitType(JuliaParser.TypeContext context)
    {
        return TypeManager.GetDataType(context.GetText());
        //return base.VisitType(context);
    }
}