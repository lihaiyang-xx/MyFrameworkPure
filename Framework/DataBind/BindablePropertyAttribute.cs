using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class BindablePropertyAttribute : Attribute
{
    public string ComponentName { get; }
    public Type ComponentType { get; }
    public string ComponentProperty { get; }
    public BindDirection Direction { get; }


    /// <summary>
    /// 定义一个可绑定字段的特性
    /// </summary>
    /// <param name="componentName">组件名称（GameObject 的名字）</param>
    /// <param name="componentType">组件的类型（如 Text, Slider 等）</param>
    /// <param name="componentProperty">组件要绑定的属性（如 text, value 等）</param>
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