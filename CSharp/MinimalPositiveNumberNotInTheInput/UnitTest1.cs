namespace MinimalPositiveNumberNotInTheInput;

public class UnitTest1
{
    [Theory]
    [InlineData(new[]{1,3,6,4,1,2}, 5)]
    [InlineData(new[]{1,2,3}, 4)]
    [InlineData(new[]{1,3,4}, 2)]
    [InlineData(new[]{2,3,4}, 1)]
    [InlineData(new[]{3,4,5}, 1)]
    [InlineData(new[]{1,3,4,2,5,7,6,9}, 8)]
    [InlineData(new[]{1,3,3,4,2,2,5,7,7,6,9}, 8)]
    public void WhenGivenTheArrayA_TheNumberBShouldBeReturned(int[] a, int b)
    {
        var solution = new Solution();
        var result = solution.solution(a);
        Assert.Equal(result, b);
    }
}
