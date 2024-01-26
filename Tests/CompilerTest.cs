namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()
                        x::Integer = 10
                        y::Integer = 20 + x
                        
                        if true
                            x = 10
                        end
                        
                        println(y)
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
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
    
    [Fact]
    private void ComplexStringExpression_DoesNotThrow()
    {
        var script = """
                     function main()
                        b::String = "World"
                        if "Hello" == b || "Hello" == b
                            println("Hello World")
                        else
                            b = "Hello"
                        end
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }

    [Fact]
    private void NestedIfWhile_DoesNotThrow()
    {
        var script = """
                     function main()
                        if true
                            x::Bool = true
                            while x
                                if x == x
                                    x = false
                                end
                            end
                        end
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
    
    [Fact]
    private void ComplexFloatExpression_DoesNotThrow()
    {
        var script = """
                     function main()
                        b::Float64 = 10.0
                        if 10.0 == b || 10.0 == b && b > 5.0
                            println("Hello World")
                        else
                            b = 10.0
                        end
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        compiler.Compile();
        
        Assert.True(true);
    }
}