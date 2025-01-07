using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class BindablePropertyAttribute : Attribute
{
    public string ComponentName { get; }
    public Type ComponentType { get; }
    public string ComponentProperty { get; }
    public BindDirection Direction { get; }


    /// <summary>
    /// ����һ���ɰ��ֶε�����
    /// </summary>
    /// <param name="componentName">������ƣ�GameObject �����֣�</param>
    /// <param name="componentType">��������ͣ��� Text, Slider �ȣ�</param>
    /// <param name="componentProperty">���Ҫ�󶨵����ԣ��� text, value �ȣ�</param>
    public BindablePropertyAttribute(string componentName, Type componentType, string componentProperty,BindDirection direction = BindDirection.Both)
    {
        ComponentName = componentName;
        ComponentType = componentType;
        ComponentProperty = componentProperty;
        Direction = direction;
    }
}

public enum BindDirection
{
    Data2View,
    View2Data,
    Both
}