using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 组件扩展
    /// </summary>
    public static class ComponentExtension
    {
        public static void SetActive(this Component component, bool active)
        {
            component.gameObject.SetActive(active);
        }
    }
}

