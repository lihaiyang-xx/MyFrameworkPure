using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BindableView : MonoBehaviour
{
    public BindableData Data { get; private set; }

    public void Bind(BindableData data)
    {
        if (data == null)
        {
            Debug.LogWarning("data is null. Binding skipped.");
            return;
        }

        Data = data;
        var fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<BindablePropertyAttribute>();
            if (attribute != null)
            {
                string componentName = attribute.ComponentName;
                Type componentType = attribute.ComponentType;
                string componentProperty = attribute.ComponentProperty;
                BindDirection bindDirection = attribute.Direction;

                var bindProperty = field.GetValue(data);
                if (bindProperty == null)
                {
                    Debug.LogWarning($"Field '{field.Name}' is null. Skipping.");
                    continue;
                }

                var component = FindComponent(componentName, componentType);
                if (component == null)
                {
                    Debug.LogWarning($"UI Component '{componentName}' of type '{componentType}' not found.");
                    continue;
                }

                var valueType = field.FieldType.GetGenericArguments()[0];
                if (bindDirection == BindDirection.Both || bindDirection == BindDirection.Data2View)
                {
                    var bindDataToViewMethod = typeof(BindableView).GetMethod(nameof(BindDataToView), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(valueType);
                    bindDataToViewMethod.Invoke(this, new[] { component, componentProperty, bindProperty });
                }
                
                if(bindDirection == BindDirection.Both || bindDirection == BindDirection.View2Data)
                {
                    var bindViewToDataMethod = typeof(BindableView).GetMethod(nameof(BindViewToData), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(valueType);
                    bindViewToDataMethod.Invoke(this, new[] { component, componentProperty, bindProperty });
                }
            }
        }
    }

    private void BindDataToView<T>(Component component, string propertyName, BindableProperty<T> bindableProperty)
    {
        if(component == null)
            return;
        var uiProperty = component.GetType().GetProperty(propertyName);
        if (uiProperty != null && uiProperty.CanWrite)
        {
            if (component is Dropdown dropdown && propertyName == "options")
            {
                bindableProperty.OnValueChanged += newValue =>
                {
                    UpdateDropdownOptions(dropdown, bindableProperty.Value);
                };
                UpdateDropdownOptions(dropdown, bindableProperty.Value);
            }
            else
            {
                bindableProperty.OnValueChanged += newValue =>
                {
                    Debug.Log(component.name + " "+newValue);
                    uiProperty.SetValue(component, newValue);
                };
                uiProperty.SetValue(component, bindableProperty.Value);
            }
        }
    }

    private void BindViewToData<T>(Component component, string propertyName, BindableProperty<T> bindableProperty)
    {
        if (component is InputField inputField)
        {
            inputField.onValueChanged.AddListener(newValue => bindableProperty.Value = (T)Convert.ChangeType(newValue, typeof(T)));
        }
        else if (component is Slider slider)
        {
            slider.onValueChanged.AddListener(newValue => bindableProperty.Value = (T)Convert.ChangeType(newValue, typeof(T)));
        }
        else if (component is Toggle toggle)
        {
            toggle.onValueChanged.AddListener(newValue => bindableProperty.Value = (T)Convert.ChangeType(newValue, typeof(T)));
        }
        else if(component is Dropdown dropdown)
        {
            dropdown.onValueChanged.AddListener(newValue=>bindableProperty.Value = (T)Convert.ChangeType(newValue, typeof(T)));
        }
    }

    private Component FindComponent(string componentName, Type componentType)
    {
        var targetTransform = transform.Find(componentName);
        if (targetTransform == null)
        {
            Debug.LogWarning($"Transform '{componentName}' not found under '{name}'.");
            return null;
        }

        return targetTransform.GetComponent(componentType);
    }

    private void UpdateDropdownOptions(Dropdown dropdown, object value)
    {
        dropdown.ClearOptions();

        if (value is List<string> stringList)
        {
            dropdown.AddOptions(stringList);
        }
        else if (value is string[] stringArray)
        {
            dropdown.AddOptions(stringArray.ToList());
        }
    }

    private void EnsureAOTCodeGeneration()
    {
        BindDataToView<int>(null, null, null);
        BindDataToView<float>(null, null, null);
        BindDataToView<string>(null, null, null);
        BindDataToView<string[]>(null, null, null);

        BindViewToData<int>(null, null, null);
        BindViewToData<float>(null, null, null);
        BindViewToData<string>(null, null, null);
        BindViewToData<string[]>(null, null, null);
    }
}