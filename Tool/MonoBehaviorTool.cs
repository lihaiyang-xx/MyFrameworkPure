using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonoUpdate
{
    void MonoUpdate();
}

public class MonoBehaviorTool : MonoBehaviour
{
    private static MonoBehaviorTool monoBehaviorTool;

    private static List<IMonoUpdate> monoUpdateList;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static MonoBehaviorTool()
    {
        if (!monoBehaviorTool)
        {
            GameObject go = new GameObject("MonoBehaviorTool");
            monoBehaviorTool = go.AddComponent<MonoBehaviorTool>();

            monoUpdateList = new List<IMonoUpdate>();
        }
    }

    /// <summary>
    /// 注册更新
    /// </summary>
    /// <param equimpentName="monoUpdate"></param>
    public static void RegisterUpdate(IMonoUpdate monoUpdate)
    {
        monoUpdateList.Add(monoUpdate);
    }
    /// <summary>
    /// 取消注册更新
    /// </summary>
    /// <param equimpentName="monoUpdate"></param>
    public static void UnRegisterUpdate(IMonoUpdate monoUpdate)
    {
        monoUpdateList.Remove(monoUpdate);
    }


    void Update()
    {
        foreach (var monoUpdate in monoUpdateList)
        {
            monoUpdate.MonoUpdate();
        }
    }

    void OnDestroy()
    {
        monoUpdateList.Clear();
        monoUpdateList = null;
        monoBehaviorTool = null;
    }
}
