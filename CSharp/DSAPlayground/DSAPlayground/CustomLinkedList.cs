using System.Collections;

namespace DSAPlayground;

public class CustomLinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }
    private CustomLinkedListItem<T>? Head { get; set; }
    private CustomLinkedListItem<T>? Tail { get; set; }

    public CustomLinkedList() { }
    public CustomLinkedList(IEnumerable<T> items)
    {
        var first = true;
        var previous = default(CustomLinkedListItem<T>);
        foreach (var item in items)
        {
            if (first)
            {
                Head = new CustomLinkedListItem<T>(item);
                previous = Head;
            }
            else
            {
                previous = new CustomLinkedListItem<T>(item, previous);
            }
            Tail = previous;

            first = false;
        }
    }

    /// <summary> Inserts a new element at the end of the list. </summary>
    public void Push(T item)
    {
        if (Tail == null)
        {
            var linkedListItem = new CustomLinkedListItem<T>(item);
            Head = linkedListItem;
            Tail = linkedListItem;
        }
        else
        {
            var linkedListItem = new CustomLinkedListItem<T>(item, Tail);
            Tail.Next = linkedListItem;
            Tail = linkedListItem;
        }
    }

    /// <summary> Gets the last element of the list and then removes it. </summary>
    public T Pop()
    {
        if (Tail == null)
        {
            throw new InvalidOperationException("The list is empty");
        }
        if (Tail.Previous != null)
        {
            var poppedValue = Tail.Value;
            Tail = Tail.Previous;
            Tail.Next = null;
            return poppedValue;
        }
        else
        {
            var poppedValue = Tail.Value;
            Head = null;
            Tail = null;
            return poppedValue;
        }
    }

    public void Append(T item)
    {
        var customLinkedListItem = new CustomLinkedListItem<T>(item);
        if (Head != null)
        {
            Head.Previous = customLinkedListItem;
            customLinkedListItem.Next = Head;
            Head = customLinkedListItem;
        }
        else
        {
            Head = customLinkedListItem;
            Tail = customLinkedListItem;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var enumerator = new CustomLinkedListEnumerator<T>(Head);
        return enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class CustomLinkedListItem<T>
{
    public T Value { get; set; }
    public CustomLinkedListItem<T>? Next { get; set; }
    public CustomLinkedListItem<T>? Previous { get; set; }

    public CustomLinkedListItem(T item, CustomLinkedListItem<T>? previous = null)
    {
        Value = item;
        Previous = previous;
        if (Previous != null)
            Previous.Next = this;
    }
}

public class CustomLinkedListEnumerator<T> : IEnumerator, IEnumerator<T>
{
    private CustomLinkedListItem<T>? _currentItem;
    private readonly CustomLinkedListItem<T>? _firstItem;
    object IEnumerator.Current => Current;
    public T Current => _currentItem.Value;

    public CustomLinkedListEnumerator(CustomLinkedListItem<T>? first)
    {
        _firstItem = first;
        _currentItem = null;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool MoveNext()
    {
        if (_currentItem?.Next != null)
        {
            _currentItem = _currentItem.Next;
            return true;
        }
        else if (_currentItem == null && _firstItem != null)
        {
            _currentItem = _firstItem;
            return true;
        }
        return false;
    }

    public void Reset()
    {
        _currentItem = null;
    }
}
