using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    public static MonoBehaviorTool GetInstanceInActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        MonoBehaviorTool instance =
            FindObjectsOfType<MonoBehaviorTool>()?.Where(x => x.gameObject.scene == activeScene).FirstOrDefault();
        if (!instance)
        {
            GameObject go = new GameObject("MonoBehaviorTool");
            instance = go.AddComponent<MonoBehaviorTool>();
        }

        return instance;
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
        if(monoUpdateList.Contains(monoUpdate))
            monoUpdateList.Remove(monoUpdate);
    }

    public void UnRegisterUpdate(UnityAction callback)
    {
        UpdateCall -= callback;
    }


    void Update()
    {
        for (int i = 0; i < monoUpdateList.Count; i++)
        {
            IMonoUpdate monoUpdate = monoUpdateList[i];
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
