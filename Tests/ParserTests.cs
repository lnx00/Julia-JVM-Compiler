using Compiler.Parser;
using Compiler.Parser.ErrorHandling;

namespace Tests;

public class ParserTests
{
    [Fact]
    public void IntegerDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::Integer = 1";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void NegativeIntegerDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::Integer = -1";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void FloatDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::Float64 = 1.0";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void NegativeFloatDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::Float64 = -1.0";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void StringDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::String = \"Hello, World!\"";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void DangerousStringDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::String = \"if true while 1 + 2 * 3.0 else else() end\"";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void BoolDeclaration_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = true";

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void IntegerAssignment_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 1
                     x = 2
                     """;

        // Act
        var parser = new Parser(script);
        parser.Parse();

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void MissingDeclarationType_ThrowsSyntaxError()
    {
        // Arange
        var script = "x:: = 1";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        var exception = Assert.Throws<SyntaxErrorException>(() => parser.Parse());
        Assert.Equal(1, exception.Line);
        Assert.Equal(4, exception.Position);
    }
    
    [Fact]
    public void SimpleIntegerAddition_DoesNotThrow()
    {
        // Arange
        var script = "x::Integer = 1 + 2";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }

    [Fact]
    public void IntegerFloatAddition_ThrowsError()
    {
        // Arange
        var script = "x::Integer = 1 + 2.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<Exception>(() => parser.Parse());
    }
}