
namespace BubbleSort.Console;

public class Sorter
{
    private readonly bool _verbose;
    public Sorter(bool verbose = false)
    {
        _verbose = verbose;
    }

    public int[] Sort(int[] unorderedList)
    {
        var orderedList = unorderedList;
        StringBuilder? verboseStringBuilder = null;
        if(_verbose)
        {
            verboseStringBuilder = new StringBuilder();
        }
        for(var i = 0; i < orderedList.Length; i++)
        {
            for(var j = 0; j < orderedList.Length - 1; j++)
            {
                if(orderedList[j] > orderedList[j + 1])
                {
                    (orderedList[j + 1], orderedList[j]) = (orderedList[j], orderedList[j + 1]);

                    verboseStringBuilder?.Append($"{orderedList[j]} ");
                }
                verboseStringBuilder?.AppendLine();
            }
        }
        if(verboseStringBuilder != null)
        {
            Console.WriteLine(verboseStringBuilder.ToString());
        }
        return orderedList;
    }
}
