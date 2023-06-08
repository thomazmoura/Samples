var sorter = new Sorter();
var unorderedList = args.Select(int.Parse).ToArray();
var orderedList = sorter.Sort(unorderedList);

Console.WriteLine(string.Join(" ", orderedList));