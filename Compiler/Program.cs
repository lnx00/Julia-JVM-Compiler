// See https://aka.ms/new-console-template for more information

using Compiler.Parser;
using Compiler.Parser.ErrorHandling;

Console.WriteLine("Hello, World!");

var input = File.ReadAllText("input.jl");

Parser parser = new(input);

try
{
    parser.Parse();
}
catch (ParserException e)
{
    Console.WriteLine($"Syntax error at line {e.Line}, position {e.Position}: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Unknown parser error: {e.Message}");
}
