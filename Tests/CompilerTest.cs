namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()::Float64
                        return 10.0
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script);
        compiler.Compile();
        
        Assert.True(true);
    }
}