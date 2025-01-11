using FluentAssertions;

namespace MissingNumber;

public class MissingNumber
{

    public int FindMissingNumber(int[] A)
    {
        Array.Sort(A);
        var lastNumber = 0;
        var areTherePositiveNumbers = false;
        for(int i = 0; i < A.Length; i++)
        {
            if(A[i] < 0)
            {
                continue;
            }

            areTherePositiveNumbers = true;

            if (A[i] - lastNumber > 1)
            {
                return lastNumber + 1;
            }

            lastNumber = A[i];
        }
        return areTherePositiveNumbers? lastNumber+1 : 1;
    }

    [Theory]
    [InlineData(new int[] { 4, 7, 3, 2, 1 }, 5)]
    [InlineData(new int[] { -5, -3 }, 1)]
    [InlineData(new int[] { -5, 3 }, 1)]
    [InlineData(new int[] { -5, 1, 2, 3 }, 4)]
    [InlineData(new int[] { 1, 2, 3 }, 4)]
    public void FindMissingNumber_WhenGivenValidInput_ShouldReturnTheSmallestInteger(int[] numberArray, int expectedNumber)
    {
        var smallestPositiveInteger = FindMissingNumber(numberArray);
        smallestPositiveInteger.Should().Be(expectedNumber);
    }
}
