namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()
                        x::Integer = 10
                        while true
                            x = 0
                        end
                        y::Integer = 10 + x
                        
                        return
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
}