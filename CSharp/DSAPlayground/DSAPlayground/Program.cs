int[] linkedListInput = [1, 2, 3];
var linkedList = new CustomLinkedList<int>(linkedListInput);
var resultado = string.Join(", ", linkedList.Select(item => item.ToString()));
Console.WriteLine($"Os elementos acrescentados na lista foram: {resultado}");
