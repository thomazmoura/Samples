using System;
using System.Collections.Generic;

public class Solution {
    HashSet<char> numberCharacters = new HashSet<char>(){'0','1','2','3','4','5','6','7','8','9'};
    public String solution(String s) {
        char c = s[0];
        var lowerC = s.ToLower()[0];
        var upperC = s.ToUpper()[0];
        if ( c == upperC && c != lowerC ) {
            return "upper";
        } else if (c == lowerC && c != upperC) {
            return "lower";
        } else if (numberCharacters.Contains(c)) {
            return "digit";
        } else {
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
