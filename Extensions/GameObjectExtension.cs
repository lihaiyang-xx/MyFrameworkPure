using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{

    public static void AddCollider<T>(this GameObject go) where T:Collider
    {
        MeshFilter filter = go.GetComponent<MeshFilter>();
        if (filter&& filter.mesh && !go.GetComponent<Collider>())
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
        if (go != null)
        {
            T[] ts = go.GetComponents<T>();
            foreach (var t in ts)
            {
                if (t != null) Object.Destroy(t);
            }
            return go.AddComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 获取父物体组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
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
    /// <param name="go"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool IsOnMouse(this GameObject go,Camera camera)
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
}
