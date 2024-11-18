using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class CustomDictionary<TKey, TValue> : IEnumerable<(TKey, TValue)>
{
    public List<CustomKeyValuePair<TKey, TValue>> dictionary = new List<CustomKeyValuePair<TKey, TValue>>();

    public int Count => dictionary.Count;

    public CustomDictionary() {
        this.dictionary = new List<CustomKeyValuePair<TKey, TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        dictionary.Add(new CustomKeyValuePair<TKey, TValue>(key, value));
    }
    
    public TValue this[TKey key]
    {
        get
        {
            return dictionary.Where(x => x.key.Equals(key)).DefaultIfEmpty(default).FirstOrDefault().value;
        }
        set
        {
            var pair = dictionary.Where(x => x.key.Equals(key)).DefaultIfEmpty(default).FirstOrDefault();
            if (pair != null)
                pair.value = value;
        }
    }

    public IEnumerator<(TKey, TValue)> GetEnumerator()
    {
        foreach (var keyValuePair in dictionary)
        {
            yield return (keyValuePair.key, keyValuePair.value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}