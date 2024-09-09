using Compiler.Core.AST;
using Compiler.Core.Common;
using Compiler.Core.SymbolTable;
using Compiler.Core.SymbolTable.Symbols;
using Compiler.Parser.ErrorHandling;

namespace Compiler.Parser.Visitor;

public class GlobalVisitor : JuliaBaseVisitor<object>
{
    private readonly SymbolTable _symbolTable;
    
    public GlobalVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }
    
    public override object VisitStart(JuliaParser.StartContext context)
    {
        // Visit all global functions
        foreach (var function in context.function())
        {
            Visit(function);
        }

        return _symbolTable;
    }

    public override object VisitFunction(JuliaParser.FunctionContext context)
    {
        var funcName = context.IDENTIFIER().GetText();
        var returnType = TypeManager.DataType.Void;
        if (context.type() != null)
        {
            // Retrieve the return type
            var returnTypeName = context.type().GetText();
            returnType = TypeManager.GetDataType(returnTypeName)
                         ?? throw SyntaxErrorException.Create($"Unknown return type {returnTypeName}", context);
        }
        
        // Check if function already exists
        if (_symbolTable.IsDefined(funcName))
        {
            throw new Exception($"Function {funcName} already defined");
        }
        
        // Add to symbol table
        var parameters = Visit(context.parameters()) as List<VariableSymbol> ?? throw SyntaxErrorException.Create("Invalid parameters", context);
        var functionSymbol = _symbolTable.AddFunction(funcName, returnType, parameters);
        
        return functionSymbol;
    }

    public override object VisitParameters(JuliaParser.ParametersContext context)
    {
        List<VariableSymbol> parameters = new();
        for (int i = 0; i < context.IDENTIFIER().Length; i++)
        {
            var varName = context.IDENTIFIER(i).GetText();
            var typeName = context.type(i).GetText();
            
            // Check if variable already exists
            if (_symbolTable.IsDefined(varName))
            {
                throw new Exception($"Variable {varName} already defined");
            }
            
            // Add to parameter list
            var varType = TypeManager.GetDataType(typeName) ?? throw SyntaxErrorException.Create($"Unknown parameter type {typeName}", context);
            parameters.Add(new VariableSymbol(varName, -1, varType));
        }
        
        return parameters;
    }
}