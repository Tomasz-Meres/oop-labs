using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 0, 10, 5)]
    [InlineData(0, 0, 10, 0)]
    [InlineData(10, 0, 10, 10)]
    [InlineData(-1, 0, 10, 0)]
    [InlineData(99, 0, 10, 10)]
    public void Limiter_ShouldReturnClampedValue(int value, int min, int max, int expected)
    {
        Assert.Equal(expected, Validator.Limiter(value, min, max));
    }


    // ---------- TESTY DLA SHORTENER ----------

    [Fact]
    public void Shortener_NullValue_ShouldBecomeUnknown()
    {
        string result = Validator.Shortener(null, 3, 10, '#');
        Assert.Equal("Unknown", result);
    }

    [Theory]
    [InlineData("   Ala   ", "Ala")]
    [InlineData("ala", "Ala")]
    [InlineData("ALA", "ALA")]
    public void Shortener_ShouldTrimAndCapitalize(string input, string expected)
    {
        string result = Validator.Shortener(input, 1, 10, '#');
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Abcdefghijkl", 3, 5, '#', "Abcde")]
    [InlineData("AlaMaKota", 3, 3, '#', "Ala")]
    public void Shortener_ShouldShortenToMax(string input, int min, int max, char ph, string expected)
    {
        string result = Validator.Shortener(input, min, max, ph);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("A", 3, 10, '#', "A##")]
    [InlineData("Test", 6, 10, '#', "Test##")]
    public void Shortener_ShouldPadToMin(string input, int min, int max, char ph, string expected)
    {
        string result = Validator.Shortener(input, min, max, ph);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Shortener_ShouldTrimAfterShortening()
    {
        string input = "   Ala     ma      kota";
        string result = Validator.Shortener(input, 2, 3, '#');

        Assert.Equal("Ala", result);
    }

    [Fact]
    public void Shortener_LowercaseFirstCharacter_ShouldBeCapitalized()
    {
        string result = Validator.Shortener("test", 1, 10, '#');
        Assert.Equal("Test", result);
    }

    [Fact]
    public void Shortener_FirstCharacterIsNonLetter_ShouldNotChangeCase()
    {
        string result = Validator.Shortener("  123abc", 1, 10, '#');
        Assert.Equal("123abc", result);
    }

    [Fact]
    public void Shortener_WhitespaceOnly_ShouldPadAndCapitalizeIfPossible()
    {
        string result = Validator.Shortener("     ", 3, 10, 'a');
        Assert.Equal("Aaa", result);
    }
}
