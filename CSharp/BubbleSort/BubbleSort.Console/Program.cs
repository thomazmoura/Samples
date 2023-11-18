var isVerbose = args.Contains("-v") || args.Contains("--verbose");
var sorter = new Sorter(isVerbose);
var unorderedList = args
    .Where(arg => int.TryParse(arg, out _))
    .Select(int.Parse)
    .ToArray();
var orderedList = sorter.Sort(unorderedList);

Console.WriteLine(string.Join(" ", orderedList));