namespace BubbleSort.Test;

public class SorterTests
{
    [Theory]
    [InlineData(new int[] { 3, 5, 1, 4, 2 }, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 3, 7, 5, 2 }, new int[] { 2, 3, 5, 7 })]
    public void Sort_WhenPassedAnUnorderedListOfInts_ShouldReturnTheSameListButOrderedInAscendingOrder(int[] unorderedList, int[] orderedList)
    {
        var sorter = new Sorter();

        var result = sorter.Sort(unorderedList);

        result.Should().BeEquivalentTo(orderedList);
    }
}