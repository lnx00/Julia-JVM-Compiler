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
    public void IntegerFloatAddition_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Integer = 1 + 2.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void SimpleIntegerSubtraction_DoesNotThrow()
    {
        // Arange
        var script = "x::Integer = 1 - 2";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void IntegerFloatSubtraction_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Integer = 1 - 2.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact (Skip = "Not required for the project")]
    public void StringConcatenation_DoesNotThrow()
    {
        // Arange
        var script = "x::String = \"Hello, \" * \"World!\"";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact (Skip = "Not required for the project")]
    public void StringIntegerConcatenation_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::String = \"Hello\" * 1";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void StringAssignment_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::String = "Hello, World!"
                     x = "Goodbye, World!"
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void IntegerAssignmentToFloatVariable_ThrowsError()
    {
        // Arange
        var script = """
                     x::Float64 = 1.0
                     x = 1
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<Exception>(() => parser.Parse());
    }
    
    [Fact]
    public void StringAssignmentToIntegerVariable_ThrowsError()
    {
        // Arange
        var script = """
                     x::Integer = 1
                     x = "Hello, World!"
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<Exception>(() => parser.Parse());
    }
    
    [Fact]
    public void ComplexIntegerMath_DoesNotThrow()
    {
        // Arange
        var script = "x::Integer = 1 + (2 * (3 - 4) / 5) % ((6))";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void ComplexFloatMath_DoesNotThrow()
    {
        // Arange
        var script = "x::Float64 = 1.0 + (2.0 * (3.0 - 4.0) / ((5.0)))";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact (Skip = "Not required for the project")]
    public void ComplexStringMath_DoesNotThrow()
    {
        // Arange
        var script = "x::String = \"Hello, \" * \"World!\" * \"Goodbye!\"";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void StringAddition_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::String = \"Hello, \" + \"World!\"";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }

    [Fact]
    public void ComplexBoolMath_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = true && false || (false && false || ((true)))";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void ComplexBoolMathWithParentheses_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = (true && false) || (false && false) || ((true))";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void BoolIntegerMath_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Bool = true && 1";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void BoolFloatMath_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Bool = true + 1.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void BoolStringMath_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Bool = true + \"Hello, World!\" || false";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void BoolAssignment_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Bool = true && false
                     x = false || true
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void IntegerVariableAssignment_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 1
                     y::Integer = x
                     y = y + 1
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void WhileLoop_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 0
                     while true && true
                         x = 1 + 2
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void ComplexWhileLoop_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Bool = true
                     while true && (true && !x)
                         x = false
                         while false || false
                             x = true
                         end
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void WrongComplexWhileLoop_ThowsTypeMismatch()
    {
        // Arange
        var script = """
                     x::Bool = true
                     while true && (true && !x)
                         x = false
                         while false || (x && !1 || true)
                             x = true
                         end
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void NotInteger_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Integer = !1";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<InvalidOperatorException>(() => parser.Parse());
    }
}