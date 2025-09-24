using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PhoneDictionary.Helper;

public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    public int Count { get; }
    public bool IsReadOnly { get; }
    public ICollection<TKey> Keys { get; }
    public ICollection<TValue> Values { get; }
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }


    public void Add(TKey key, TValue value)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        throw new NotImplementedException();
    }

    public TValue this[TKey key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }


}