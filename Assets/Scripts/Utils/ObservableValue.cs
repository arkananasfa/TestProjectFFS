using System;

public class ObservableValue<T>
{

    public event Action<T> OnValueChanged;

    private T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    private T _value;

    private ObservableValue(T value)
    {
        Value = value;
    }
    
    public static implicit operator T (ObservableValue<T> v) => v.Value;
    public static implicit operator ObservableValue<T>(T v) => new (v);
    
}