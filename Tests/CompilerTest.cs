namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()
                        x::Integer = 10 + 5
                        if true
                            println("Hello World!")
                        end
                        y::Integer = 10
                        
                        return
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
}