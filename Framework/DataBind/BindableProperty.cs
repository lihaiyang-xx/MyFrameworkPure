using System;

public class BindableProperty<T>
{
    private T _value;

    /// <summary>
    /// ������ֵ�����仯ʱ�������¼���
    /// </summary>
    public event Action<T> OnValueChanged;

    /// <summary>
    /// ����ֵ��֧�ּ���ֵ�ı仯��
    /// </summary>
    public T Value
    {
        get => _value;
        set
        {
            if (!Equals(_value, value))
            {
                _value = value;
                OnValueChanged?.Invoke(_value); // ����ֵ�仯�¼�
            }
        }
    }

    /// <summary>
    /// ���캯�������ó�ʼֵ��
    /// </summary>
    /// <param name="initialValue">���Եĳ�ʼֵ��</param>
    public BindableProperty(T initialValue = default)
    {
        _value = initialValue;
    }

    /// <summary>
    /// �ֶ�����֪ͨ�¼�������ǿ��ˢ�°󶨡�
    /// </summary>
    public void Notify()
    {
        OnValueChanged?.Invoke(_value);
    }

    /// <summary>
    /// ��ʽ���������أ�֧�ִ� BindableProperty<T> �Զ�ת��Ϊ�������͡�
    /// </summary>
    public static implicit operator T(BindableProperty<T> bindableProperty)
    {
        return bindableProperty._value;
    }
}