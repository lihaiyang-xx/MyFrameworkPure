using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtension
{
    public static void SetActive(this Component component, bool active)
    {
        component.gameObject.SetActive(active);
    }
}
