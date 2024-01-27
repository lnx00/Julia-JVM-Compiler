﻿using Compiler.CodeGenerator;
using Compiler.Core.AST;
using Compiler.Core.IntermediateCode;
using Compiler.Parser.ErrorHandling;

namespace Compiler;

public class Compiler
{
    private readonly Parser.Parser _parser;
    private readonly CodeGenerator.CodeGenerator _codeGenerator;
    private readonly string _name;
    
    public Compiler(string sourceCode, string name)
    {
        _parser = new Parser.Parser(sourceCode);
        _codeGenerator = new CodeGenerator.CodeGenerator();
        _name = name;
    }

    private StartNode Parse()
    {
        return _parser.Parse();
    }
    
    private List<Instruction> GenerateCode(StartNode ast)
    {
        return _codeGenerator.Generate(ast, _name);
    }
    
    public string Compile()
    {
        var ast = Parse();
        var instructions = GenerateCode(ast);
        var code = string.Join("\n", instructions.Select(i => i.Translate()));

        return code;
    }
    
    public int LivenessAnalysis()
    {
        /*var ast = Parse();
        return _parser.GetSymbolTable().VariableCount;*/
        
        var ast = Parse();
        var registers = LivenessAnalyzer.Analyze(ast);
        
        return registers;
    }
}