using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;

public interface IMonoUpdate
{
    void MonoUpdate();
}

public class MonoBehaviorTool : CSingletonMono<MonoBehaviorTool>
{
    private  List<IMonoUpdate> monoUpdateList;

    private  event UnityAction UpdateCall;

    void Awake()
    {
        monoUpdateList = new List<IMonoUpdate>();
    }

    /// <summary>
    /// 注册更新
    /// </summary>
    /// <param equimpentName="monoUpdate"></param>
    public void RegisterUpdate(IMonoUpdate monoUpdate)
    {
        monoUpdateList.Add(monoUpdate);
    }

    public void RegisterUpdate(UnityAction callback)
    {
        UpdateCall += callback;
    }
    
    /// <summary>
    /// 取消注册更新
    /// </summary>
    /// <param equimpentName="monoUpdate"></param>
    public void UnRegisterUpdate(IMonoUpdate monoUpdate)
    {
        monoUpdateList.Remove(monoUpdate);
    }

    public void UnRegisterUpdate(UnityAction callback)
    {
        UpdateCall -= callback;
    }


    void Update()
    {
        foreach (var monoUpdate in monoUpdateList)
        {
            monoUpdate.MonoUpdate();
        }

        if (UpdateCall != null)
            UpdateCall();
    }

    void OnDestroy()
    {
        monoUpdateList.Clear();
        monoUpdateList = null;
        UpdateCall = null;
    }
}
