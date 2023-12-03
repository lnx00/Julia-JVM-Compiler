using Compiler.Parser;
using Compiler.Parser.ErrorHandling;

var input = File.ReadAllText("input.jl");

Parser parser = new(input);

try
{
    parser.Parse();
}
catch (ParserException e)
{
    Console.WriteLine($"Error at line {e.Line}, position {e.Position}: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Unknown parser error: {e.Message}");
}
