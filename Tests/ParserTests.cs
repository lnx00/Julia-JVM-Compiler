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
    public void IntegerDeclarationFromFloat_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Integer = 1.0";

        // Act
        var parser = new Parser(script);

        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
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
    public void FloatDeclarationFromString_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Float64 = \"Hello, World!\"";

        // Act
        var parser = new Parser(script);

        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
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
    public void StringDeclarationFromBool_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::String = true";

        // Act
        var parser = new Parser(script);

        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
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
    public void IntegerAssignmentToFloatVariable_ThrowsTypeMismatch()
    {
        // Arange
        var script = """
                     x::Float64 = 1.0
                     x = 1
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void StringAssignmentToIntegerVariable_ThrowsTypeMismatch()
    {
        // Arange
        var script = """
                     x::Integer = 1
                     x = "Hello, World!"
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
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
    
    [Fact]
    public void NotFloat_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Float64 = !1.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<InvalidOperatorException>(() => parser.Parse());
    }
    
    [Fact]
    public void IntComparison_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = 1 < 2 && 1 <= 2 && 1 > 2 && 1 >= 2 && 1 == 2 && 1 != 2";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void ComplexIntComparison_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = 1 < 2 && (1 <= 2 && (1 > 2 && (1 >= 2 && (1 == 2 && (1 != 2)))))";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void IntFloatComparison_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Bool = 1 < 2.0 && 1 <= 2.0 && 1 > 2.0 && 1 >= 2.0 && 1 == 2.0 && 1 != 2.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void FloatComparisonToFloatVariable_ThrowsTypeMismatch()
    {
        // Arange
        var script = "x::Float64 = 1.0 < 2.0";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void StringComparison_DoesNotThrow()
    {
        // Arange
        var script = "x::Bool = \"a\" == \"a\" && \"a\" != \"b\"";
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void StringArithmeticComparison_ThrowsInvalidOperator()
    {
        // Arange
        var script = "x::Bool = \"a\" < \"b\" || \"a\" <= \"b\" || \"a\" > \"b\" || \"a\" >= \"b\"";
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<InvalidOperatorException>(() => parser.Parse());
    }

    [Fact]
    public void ComplexComparison_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 10
                     y::Float64 = 5.0
                     z::Bool = true
                     result::Bool = (x > 5) && ((y < 10.0) || (y > 0.0)) && (z != false)
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void IfStatement_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 10
                     if x > 5 && x < 15
                         x = 5
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void NestedIfStatement_DoesNotThrow()
    {
        // Arange
        var script = """
                     x::Integer = 10
                     if x > 5 && x < 15
                         if x > 10
                             x = 5
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
    public void FloatToIntegerAssignmentInNestedIfStatement_ThrowsTypeMismatch()
    {
        // Arange
        var script = """
                     x::Integer = 10
                     if x > 5 && x < 15
                         if x > 10
                             x = 5.0
                         end
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void Comments_DoesNotThrow()
    {
        // Arange
        var script = """
                     # This is a comment
                     x::Integer = 10 # This is another comment
                     if x > 5 && x < 15
                         if x > 10
                             x = 5 # This is a third comment
                             # x = 5.0
                             x = 10
                             #==
                             Hello,
                             World!
                             x = 1.0
                             ==#
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
    public void FunctionDeclaration_DoesNotThrow()
    {
        // Arange
        var script = """
                     function helloWorld()
                         x::Integer = 10
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }

    [Fact]
    public void FunctionWithParameter_DoesNotThrow()
    {
        // Arange
        var script = """
                     function helloWorld(p::Integer)
                         p = 10
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void FunctionWithMultipleParameters_DoesNotThrow()
    {
        // Arange
        var script = """
                     function helloWorld(p::Integer, q::Float64, r::String, s::Bool)
                         p = 10
                         q = 10.0
                         r = "Hello, World!"
                         s = true
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void FunctionParameterTypeMismatch_ThrowsTypeMismatch()
    {
        // Arange
        var script = """
                     function helloWorld(p::Float64)
                         p = false
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void FunctionReturnType_DoesNotThrow()
    {
        // Arange
        var script = """
                     function helloWorld()::Integer
                         x::Integer = 10
                         return x
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void FunctionParameterAccessOutsideFunction_ThrowsUndefinedVar()
    {
        // Arange
        var script = """
                     function helloWorld(p::Integer)
                         p = 10
                     end
                     p = 10
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<UndefinedVarException>(() => parser.Parse());
    }
    
    [Fact]
    public void FunctionReturnTypesMismatch_ThrowsTypeMismatch()
    {
        // Arange
        var script = """
                     function helloWorld()::Integer
                         return 10.0
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<TypeMismatchException>(() => parser.Parse());
    }
    
    [Fact]
    public void FunctionReturnWithoutType_ThrowsSyntaxError()
    {
        // Arange
        var script = """
                     function helloWorld()
                         return 10
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<SyntaxErrorException>(() => parser.Parse());
    }
    
    [Fact]
    public void IfOutOfScope_ThrowsUndefinedVar()
    {
        // Arange
        var script = """
                     if true
                         x::Integer = 10
                     end
                     x = 10
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<UndefinedVarException>(() => parser.Parse());
    }
    
    [Fact]
    public void WhileOutOfScope_ThrowsUndefinedVar()
    {
        // Arange
        var script = """
                     while true
                         x::Integer = 10
                     end
                     x = 10
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<UndefinedVarException>(() => parser.Parse());
    }
    
    [Fact]
    public void BlockOutOfScope_ThrowsUndefinedVar()
    {
        // Arange
        var script = """
                     begin
                         x::Integer = 10
                     end
                     x = 10
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<UndefinedVarException>(() => parser.Parse());
    }
    
    [Fact]
    public void BlockVariableDeclaration_DoesNotThrow()
    {
        // Arange
        var script = """
                     begin
                         x::Integer = 10
                         x = 10
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void BlockVariableCanBeRedeclared_DoesNotThrow()
    {
        // Arange
        var script = """
                     begin
                         x::Integer = 10
                     end
                     x::Integer = 5
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void BlockVariableCanBeRedeclaredWithDifferentType_DoesNotThrow()
    {
        // Arange
        var script = """
                     begin
                         x::Integer = 10
                     end
                     x::Float64 = 5.0
                     """;
        
        // Act
        var parser = new Parser(script);
        parser.Parse();
        
        // Assert
        Assert.True(true);
    }

    [Fact]
    public void BlockVariableRedeclare_ThrowsError()
    {
        // Arange
        var script = """
                     begin
                         x::Integer = 10
                         x::Integer = 5
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<Exception>(() => parser.Parse());
    }
    
    [Fact]
    public void VariableRedeclareInBlock_ThrowsError()
    {
        // Arange
        var script = """
                     x::Integer = 10
                     begin
                         x::Integer = 5
                     end
                     """;
        
        // Act
        var parser = new Parser(script);
        
        // Assert
        Assert.Throws<Exception>(() => parser.Parse());
    }
}