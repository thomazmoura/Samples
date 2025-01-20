#pragma warning disable CA1861
namespace DSAPlayground.Tests;

public class CustomLinkedLinkTests
{
    [Fact]
    public void New_WhenCalledWithoutParameters_ShouldCreateAnEmptyList()
    {
        var customLinkedList = new CustomLinkedList<int>();

        customLinkedList.Count.ShouldBe(0);
    }

    [Fact]
    public void New_WhenCalledWithAnExistingArray_ShouldCreateANewLinkedListEquivalentToTheInput()
    {
        int[] input = [1, 2, 3];
        var customLinkedList = new CustomLinkedList<int>(input);

        customLinkedList.ShouldBe(input);
    }

    [Theory]
    [InlineData(new int[0], 5, new int[] { 5 })]
    [InlineData(new int[] { 1, 2, 3 }, 5, new int[] { 1, 2, 3, 5 })]
    public void Push_WhenCalledWithAnExistingArray_ShouldCreateANewLinkedListEquivalentToTheInput(int[] input, int newValue, int[] output)
    {
        var customLinkedList = new CustomLinkedList<int>(input);

        customLinkedList.Push(newValue);

        customLinkedList.ShouldBe(output);
    }

    [Theory]
    [InlineData(new int[] { 5 }, 5)]
    [InlineData(new int[] { 1, 2, 3, 5 }, 5)]
    public void Pop_WhenCalledWithAnExistingArray_ShouldReturnTheLastValueOfTheArray(int[] input, int expectedValue)
    {
        var customLinkedList = new CustomLinkedList<int>(input);

        var poppedValue = customLinkedList.Pop();

        poppedValue.ShouldBe(expectedValue);
    }

    [Theory]
    [InlineData(new int[] { 5 }, new int[0])]
    [InlineData(new int[] { 1, 2, 3, 5 }, new int[] { 1, 2, 3 })]
    public void Pop_WhenCalledWithAnExistingArray_ShouldRemoveTheLastValueOfTheArray(int[] input, int[] output)
    {
        var customLinkedList = new CustomLinkedList<int>(input);

        customLinkedList.Pop();

        customLinkedList.ShouldBe(output);
    }

    [Theory]
    [InlineData(new int[0], 5, new int[] { 5 })]
    [InlineData(new int[] { 1, 2, 3 }, 5, new int[] { 5, 1, 2, 3 })]
    public void Append_WhenPassingValidValues_ShouldInsertAnItemInTheBeggining(int[] input, int newValue, int[] output)
    {
        var customLinkedList = new CustomLinkedList<int>(input);

        customLinkedList.Append(newValue);

        customLinkedList.ShouldBe(output);
    }
}
