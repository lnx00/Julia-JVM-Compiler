namespace Compiler;

public class Visitor : JuliaBaseVisitor<object?>
{
    private readonly Dictionary<string, object?> _variables = new();
    
    public override object? VisitDeclaration(JuliaParser.DeclarationContext context)
    {
        string varName = context.IDENTIFIER().GetText();
        var typeName = context.type().GetText();
        object? value = Visit(context.expression());
        
        _variables.Add(varName, value);

        //return base.VisitDeclaration(context);
        return null;
    }

    public override object? VisitAssignment(JuliaParser.AssignmentContext context)
    {
        string varName = context.IDENTIFIER().GetText();
        object? value = Visit(context.expression());

        if (_variables.ContainsKey(varName))
        {
            _variables[varName] = value;
        }
        else
        {
            throw new Exception($"Variable {varName} does not exist");
        }
        
        //return base.VisitAssignment(context);
        return null;
    }
}