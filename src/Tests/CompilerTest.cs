namespace Tests;

public class CompilerTest
{
    [Fact]
    private void Test_1()
    {
        var script = """
                     function main()
                        z::Integer = 10
                        x::Integer = 0
                        y::Integer = 1
                        
                        while x < 10
                            z = x * 2 + y
                            x = x + 1
                            x = 5
                            x = 1
                            # x = x + 1
                            # y = x + z
                        end
                        
                        begin
                            x2::Integer = 0
                        end
                        
                        d0::Integer = z
                        d1::Integer = 1 + d0
                        d2::Integer = 2 + d1
                        d3::Integer = 3 + d2 + x
                        d4::Integer = 4 + d3 + d2 + d1 + d0
                        
                        #println(d3)
                        #println(x)
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
    
    [Fact]
    private void LivenessAnalysis_DoesNotThrow()
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
        compiler.LivenessAnalysis();
        
        Assert.True(true);
    }
    
    [Fact]
    private void SimpleLiveness_IsCorrect()
    {
        var script = """
                     function main()
                        x::Integer = 10
                        y::Integer = 20
                        z::Integer = 30
                        
                        if x == y
                            z = x + y
                        else
                            z = x - y
                        end
                        
                        println(z)
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        var registers = compiler.LivenessAnalysis();
        
        Assert.Equal(3, registers);
    }
    
    [Fact]
    private void ComplexLiveness_IsCorrect()
    {
        var script = """
                     function main()
                        x::Integer = 10
                        y::Integer = 20
                        z::Integer = 30
                        
                        if x == y
                            z = x + y
                        else
                            z = x - y
                        end
                        
                        while x < 10
                            z = x * 2 + y
                            x = x + 1
                        end
                        
                        begin
                            x2::Integer = 0
                        end
                        
                        d0::Integer = z
                        d1::Integer = 1 + d0
                        d2::Integer = 2 + d1
                        d3::Integer = 3 + d2 + x
                        d4::Integer = 4 + d3 + d2 + d1
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        var registers = compiler.LivenessAnalysis();
        
        Assert.Equal(3, registers);
    }

    [Fact]
    private void Liveness_NoUsages_IsCorrect()
    {
        var script = """
                     function main()
                        v0::Integer = 1
                        v1::Integer = 1
                        v2::Integer = 1
                        v3::Integer = 1
                        v4::Integer = 1
                        v5::Integer = 1
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        var registers = compiler.LivenessAnalysis();
        
        Assert.Equal(1, registers);
    }
    
    [Fact]
    private void Liveness_WithUsages_IsCorrect()
    {
        var script = """
                     function main()
                        v0::Integer = 1
                        v1::Integer = 1
                        v2::Integer = 1
                        v3::Integer = 1
                        v4::Integer = 1
                        v5::Integer = 1
                        
                        v0 = v1 + v2
                        v1 = v2 + v3
                        v2 = v3 + v4
                        v3 = v4 + v5
                        v4 = v5 + v0
                        v5 = v0 + v1
                     end
                     """;
        
        var compiler = new Compiler.Compiler(script, "Test");
        var registers = compiler.LivenessAnalysis();
        
        Assert.Equal(6, registers);
    }
}