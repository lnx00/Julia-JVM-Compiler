// See https://aka.ms/new-console-template for more information

using Compiler.Parser;

Console.WriteLine("Hello, World!");

var input = File.ReadAllText("input.jl");

Parser parser = new(input);
parser.Parse();
