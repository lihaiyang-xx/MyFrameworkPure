using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class CommonTool
{
    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect(); 
    }

    /// <summary>
    /// 查找所有物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject[] FindGameobjects(string name)
    {
        List<GameObject> goList = new List<GameObject>();
        Transform[] trs = Object.FindObjectsOfType<Transform>();
        foreach (Transform tr in trs)
        {
            if (tr.name == name)
            {
                goList.Add(tr.gameObject);
            }
        }
        return goList.ToArray();
    }
}
