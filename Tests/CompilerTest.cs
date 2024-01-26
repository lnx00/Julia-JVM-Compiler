namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()
                        x::Integer = 10
                        y::Integer = 20
                        z::Integer = 30
                        
                        if true
                            x = 1
                        end
                        
                        return
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }

    [Fact]
    private void IfWithStringExpression_DoesNotThrow()
    {
        var script = """
                     function main()
                        if "Hello" == "World"
                            println("Hello World")
                        end
                        
                        return
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
}