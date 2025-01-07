using System;

public class BindableProperty<T>
{
    private T _value;

    /// <summary>
    /// 当属性值发生变化时触发的事件。
    /// </summary>
    public event Action<T> OnValueChanged;

    /// <summary>
    /// 属性值，支持监听值的变化。
    /// </summary>
    public T Value
    {
        get => _value;
        set
        {
            if (!Equals(_value, value))
            {
                _value = value;
                OnValueChanged?.Invoke(_value); // 触发值变化事件
            }
        }
    }

    /// <summary>
    /// 构造函数，设置初始值。
    /// </summary>
    /// <param name="initialValue">属性的初始值。</param>
    public BindableProperty(T initialValue = default)
    {
        _value = initialValue;
    }

    /// <summary>
    /// 手动触发通知事件，用于强制刷新绑定。
    /// </summary>
    public void Notify()
    {
        OnValueChanged?.Invoke(_value);
    }

    /// <summary>
    /// 隐式操作符重载，支持从 BindableProperty<T> 自动转换为基础类型。
    /// </summary>
    public static implicit operator T(BindableProperty<T> bindableProperty)
    {
        return bindableProperty._value;
    }
}