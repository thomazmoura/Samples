using System;
using System.Collections.Generic;

public class Solution
{
    public String solution(String s)
    {
        char c = s[0];
        if (Char.IsUpper(c))
        {
            return "upper";
        }
        else if (Char.IsLower(c))
        {
            return "lower";
        }
        else if (Char.IsDigit(c))
        {
            return "digit";
        }
        else
        {
            return "other";
        }
    }

    [Theory]
    [InlineData("a", "lower")]
    [InlineData("abc", "lower")]
    [InlineData("ABC", "upper")]
    [InlineData("Abc", "upper")]
    [InlineData("aBC", "lower")]
    [InlineData("123", "digit")]
    [InlineData("~", "other")]
    [InlineData("!", "other")]
    public void WhenGivenAString_ShouldReturnBString(string a, string b)
    {
        var solution = new Solution();
        var result = solution.solution(a);
        Assert.Equal(b, result);
    }
}
