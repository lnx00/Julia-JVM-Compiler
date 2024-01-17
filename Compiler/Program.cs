// Handle command line arguments

using Compiler.Parser.ErrorHandling;

if (args.Length == 2)
{
    var option = args[0];
    var file = args[1];
    
    if (!File.Exists(file))
    {
        Console.WriteLine($"File '{file}' does not exist");
        return;
    }

    // Initialize the compiler
    var input = File.ReadAllText(file);
    Compiler.Compiler compiler = new(input);

    try
    {
        switch (option)
        {
            case "-compile":
            {
                var code = compiler.Compile();
                var outputFile = Path.ChangeExtension(file, ".j");
                File.WriteAllText(outputFile, code);
                break;
            }
        
            case "-liveness":
            {
                int registers = compiler.LivenessAnalysis();
                Console.WriteLine($"Registers: {registers}");
                break;
            }
        
            default:
            {
                Console.WriteLine($"Unknown option '{option}'");
                break;
            }
        }
    }
    catch (ParserException e)
    {
        Console.Error.WriteLine($"Error at line {e.Line}, position {e.Position}: {e.Message}");
    }
    catch (Exception e)
    {
        Console.Error.WriteLine($"Unknown parser error: {e.Message}");
    }
}
else
{
    Console.WriteLine("""
                      Usage: Compiler <option> <file>
                      
                      Options:
                      -compile <file> - compiles the file to assembly
                      -liveness <file> - prints the liveness analysis of the file
                      """);
    return;
}
