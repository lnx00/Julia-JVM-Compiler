// See https://aka.ms/new-console-template for more information

using Antlr4.Runtime;
using Compiler;

Console.WriteLine("Hello, World!");

var input = File.ReadAllText("input.jl");

AntlrInputStream inputStream = new AntlrInputStream(input);
JuliaLexer juliaLexer = new JuliaLexer(inputStream);
CommonTokenStream commonTokenStream = new CommonTokenStream(juliaLexer);
JuliaParser juliaParser = new JuliaParser(commonTokenStream);

var startContext = juliaParser.start();
Visitor visitor = new Visitor();
visitor.Visit(startContext);