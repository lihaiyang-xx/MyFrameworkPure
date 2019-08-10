using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏对象扩展类
/// </summary>
public static class GameObjectExtension
{
    public static void AddCollider<T>(this GameObject go) where T : Collider
    {
        MeshFilter filter = go.GetComponent<MeshFilter>();
        if (filter && filter.mesh && !go.GetComponent<Collider>())
        {

            go.AddComponent<T>();
        }
        if (go.transform.childCount > 0)
        {
            foreach (Transform child in go.transform)
            {
                AddCollider<T>(child.gameObject);
            }
        }
    }
    /// <summary>
    /// 添加唯一性组件
    /// </summary>
    public static T Add<T>(this GameObject go) where T : Component
    {
        T[] ts = go.GetComponents<T>();
        foreach (var t in ts)
        {
            if (t != null)
            {
#if UNITY_EDITOR
                Object.DestroyImmediate(t);//立即销毁这个组件 t;
#else
                Object.Destroy(t);
#endif
            }
        }
        return go.AddComponent<T>();
    }

    /// <summary>
    /// 获取父物体组件
    /// </summary>
    /// <typeparam equimpentName="T">什么组件</typeparam>
    /// <param equimpentName="go">需要获取父物体组件的对象</param>
    /// <returns></returns>
    public static T GetComponentFromParent<T>(this GameObject go) where T : Component
    {
        T compoent = null;
        while ((compoent = go.GetComponent<T>()) == null && go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
        }
        return compoent;
    }

    /// <summary>
    /// 判断游戏物体是否在鼠标下
    /// </summary>
    /// <param equimpentName="go">游戏对象</param>
    /// <param equimpentName="camera"></param>
    /// <returns></returns>
    public static bool IsOnMouse(this GameObject go, Camera camera)
    {
        if (!camera)
        {
            Debug.LogError("没有合适的相机！");
            return false;
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.Equals(go) || hitInfo.transform.IsChildOf(go.transform))
                return true;
        }
        return false;
    }

    public static bool IsOnMouseDown(this GameObject go, Camera camera)
    {
        return Input.GetMouseButton(0) && go.IsOnMouse(camera);
    }

    public static Bounds GetBounds(this GameObject go)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
        renderers.ForEach((x) =>
        {
            if (bounds.size == Vector3.zero)
            {
                bounds = x.bounds;
            }
            bounds.Encapsulate(x.bounds);
        });
        return bounds;
    }

    public static T GetOrAddCompoent<T>(this GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (!t)
            t = go.AddComponent<T>();
        return t;
    }
}
