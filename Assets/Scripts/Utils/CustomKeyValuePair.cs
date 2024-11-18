using System;

[Serializable]
public class CustomKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public CustomKeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}
