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
}