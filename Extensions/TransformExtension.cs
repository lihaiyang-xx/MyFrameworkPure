using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// 设置位置坐标分量
    /// </summary>
    /// <param name="t"></param>
    /// <param name="pivot">分量字符,x,y,z</param>
    /// <param name="v">分量值</param>
    public static void SetPos(this Transform t,char pivot, float v)
    {
        Vector3 pos = t.position;
        switch (Char.ToLower(pivot))
        {
            case 'x':
                pos.x = v;
                break;
            case 'y':
                pos.y = v;
                break;
            case 'z':
                pos.z = v;
                break;
            default:
                Debug.Log("位置轴参数不正确!");
                break;
        }

        t.position = pos;
    }
    /// <summary>
    /// 设置X坐标位置
    /// </summary>
    /// <param name="t"></param>
    /// <param name="x"></param>
    public static void SetPosX(this Transform t, float x)
    {
        Vector3 v = t.position;
        v.x = x;
        t.position = v;
    }
    /// <summary>
    /// 设置Y坐标位置
    /// </summary>
    /// <param name="t"></param>
    /// <param name="y"></param>
    public static void SetPosY(this Transform t, float y)
    {
        Vector3 v = t.position;
        v.y = y;
        t.position = v;
    }
    /// <summary>
    /// 设置Z坐标位置
    /// </summary>
    /// <param name="t"></param>
    /// <param name="z"></param>
    public static void SetPosZ(this Transform t, float z)
    {
        Vector3 v = t.position;
        v.z = z;
        t.position = v;
    }

    /// <summary>
    /// 获取第level级父物体
    /// </summary>
    /// <param name="self"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static Transform GetParentByLevel(this Transform self, int level)
    {
        Transform t = self;
        while (level-- > 0)
        {
            if (t.parent == null)
            {
                return null;
            }
            t = t.parent;
        }
        return t;
    }

    /// <summary>
    /// 搜索子物体组件
    /// </summary>
    public static T Get<T>(this Transform t, string subnode) where T : Component
    {
        if (t != null)
        {
            Transform sub = t.Find(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 扩展方法 查找所有父物体
    /// 若未找到，返回-1
    /// 若找到，返回相隔层数（临近子物体于其父物体相隔1层）
    /// outNearChild表示找到的父物体的与其相隔1层的子物体
    /// </summary>
    /// <param name="self"></param>
    /// <param name="parentName"></param>
    /// <param name="NearChild"></param>
    /// <returns></returns>
    public static int FindParents(this Transform self, string parentName, out Transform NearChild)
    {
        NearChild = null;
        Transform t = self;
        int level = 0;
        while (t.parent != null)
        {
            level++;
            t = t.parent;
            if (t.name == parentName)
            {
                return level;
            }
            NearChild = t;
        }
        return -1;
    }

    /// <summary>
    /// 清除所有子物体
    /// </summary>
    /// <param name="t"></param>
    /// <param name="immediate">是否立刻销毁</param>
    public static void ClearChild(this Transform t, bool immediate = false)
    {
        if (!t) return;
        for (int i = t.childCount - 1; i >= 0; i--)
        {
            if (immediate)
            {
                UnityEngine.Object.DestroyImmediate(t.GetChild(i).gameObject);
            }
            else
            {
                UnityEngine.Object.Destroy(t.GetChild(i).gameObject);
            }
        }
    }
}
