using System.Collections;

namespace MyList;

public class MyList<T> : IList<T>
{
    private T[] _items = new T[8];

    public MyList()
    {
        Count = 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void Resize()
    {
        var newArray = new T[_items.Length * 2];
        Array.Copy(_items, newArray, _items.Length);
        _items = newArray;
    }

    public void Add(T item)
    {
        if (Count >= _items.Length)
        {
            Resize();
        }

        _items[Count] = item;
        Count++;
    }

    public void Clear()
    {
        Count = 0;
    }

    public bool Contains(T item)
    {
        return IndexOf(item) >= 0;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < Count) 
            throw new ArgumentException("The number of elements in the source MyList is greater than the available space.");

        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = _items[i];
        }
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index < 0) return false;

        RemoveAt(index);
        return true;
    }

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_items[i], item))
                return i;
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count) 
            throw new ArgumentOutOfRangeException(nameof(index));

        if (Count >= _items.Length)
        {
            Resize();
        }

        for (int i = Count; i > index; i--)
        {
            _items[i] = _items[i - 1];
        }

        _items[index] = item;
        Count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count) 
            throw new ArgumentOutOfRangeException(nameof(index));

        for (int i = index; i < Count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        Count--;
        _items[Count] = default!;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count) 
                throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= Count) 
                throw new ArgumentOutOfRangeException(nameof(index));
            _items[index] = value;
        }
    }
}
