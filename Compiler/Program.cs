// Handle command line arguments
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

    switch (option)
    {
        case "-compile":
        {
            compiler.Compile();
            break;
        }
        
        case "-liveness":
        {
            compiler.LivenessAnalysis();
            break;
        }
        
        default:
        {
            Console.WriteLine($"Unknown option '{option}'");
            break;
        }
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
