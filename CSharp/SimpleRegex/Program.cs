using System.Text.RegularExpressions;

namespace SimpleRegex;

public class Program
{
    public static string KeepOnlyNumeric(string input)
    {
        var numericOnlyPattern = @"\D";
        return Regex.Replace(input, numericOnlyPattern, "");
    }
}

public class ProgramTests
{
    [Theory]
    [InlineData("000.000.000-00", "00000000000")]
    public void KeepOnlyNumeric_ShouldReturnJustTheNumericCharacters(string input, string expectedOutput)
    {
        var result = Program.KeepOnlyNumeric(input);
        result.Should().Be(expectedOutput);
    }
}
