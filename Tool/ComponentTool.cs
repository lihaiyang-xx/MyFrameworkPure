using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentTool : MonoBehaviour
{

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanRead || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return (T) dst;
    }

}
