using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PhoneDictionary.Helper;

public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private KeyValuePair<TKey, TValue>[] items;
    private int ItemsInUse = 0;

    private const int DefaultCapacity = 16;

    public CustomDictionary(int capacity = DefaultCapacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

        items = new KeyValuePair<TKey, TValue>[capacity];
    }

    public int Count => ItemsInUse;
    public bool IsReadOnly => false;

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < ItemsInUse; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Resize()
    {
        int newSize = items.Length * 2;
        var newArray = new KeyValuePair<TKey, TValue>[newSize];
        Array.Copy(items, newArray, ItemsInUse);
        items = newArray;
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        if (item.Key == null)
            throw new ArgumentNullException(nameof(item.Key), "Key cannot be null.");
        if (TryGetIndexOfKey(item.Key, out _))
            throw new ArgumentException("An item with the same key has already been added.");

        if (ItemsInUse == items.Length)
            Resize();

        items[ItemsInUse++] = item;
    }

    public void Clear() => ItemsInUse = 0;

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        if (item.Key == null) return false;
        return TryGetIndexOfKey(item.Key, out int index) && EqualityComparer<TValue>.Default.Equals(items[index].Value, item.Value);
    }

    private bool TryGetIndexOfKey(TKey key, out int index)
    {
        for (index = 0; index < ItemsInUse; index++)
        {
            if (EqualityComparer<TKey>.Default.Equals(items[index].Key, key))
                return true;
        }
        return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < ItemsInUse)
            throw new ArgumentException("The number of elements in the source CustomDictionary is greater than the available space.");

        for (int i = 0; i < ItemsInUse; i++)
        {
            array[arrayIndex + i] = items[i];
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (item.Key == null) return false;
        if (!TryGetIndexOfKey(item.Key, out int index)) return false;

        if (!EqualityComparer<TValue>.Default.Equals(items[index].Value, item.Value))
            return false;

        for (int i = index; i < ItemsInUse - 1; i++)
        {
            items[i] = items[i + 1];
        }
        items[--ItemsInUse] = default;
        return true;
    }

    public void Add(TKey key, TValue value) => Add(new KeyValuePair<TKey, TValue>(key, value));

    public bool ContainsKey(TKey key) => key != null && TryGetIndexOfKey(key, out _);

    public bool Remove(TKey key)
    {
        if (key == null) return false;
        if (!TryGetIndexOfKey(key, out int index)) return false;

        for (int i = index; i < ItemsInUse - 1; i++)
        {
            items[i] = items[i + 1];
        }
        items[--ItemsInUse] = default;
        return true;
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (key != null && TryGetIndexOfKey(key, out int index))
        {
            value = items[index].Value;
            return true;
        }
        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetIndexOfKey(key, out int index))
                return items[index].Value;

            throw new KeyNotFoundException("The given key was not present in the dictionary.");
        }
        set
        {
            if (TryGetIndexOfKey(key, out int index))
            {
                items[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public ICollection<TKey> Keys
    {
        get
        {
            var keys = new List<TKey>(ItemsInUse);
            for (int i = 0; i < ItemsInUse; i++)
            {
                keys.Add(items[i].Key);
            }
            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            var values = new List<TValue>(ItemsInUse);
            for (int i = 0; i < ItemsInUse; i++)
            {
                values.Add(items[i].Value);
            }
            return values;
        }
    }
}
