using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 节点基类
/// </summary>
public abstract class FlowNode
{
    public FlowChart Chart { get; set; }//流程图
    /// <summary>
    /// 进入执行
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 每帧更新
    /// </summary>
    public virtual void Update()
    {

    }
    /// <summary>
    /// 退出执行
    /// </summary>
    public virtual void Exit()
    {
        Debug.Assert(Chart != null, "Chart cant be null!");
        // ReSharper disable once PossibleNullReferenceException
        Chart.ExecuteNext();//流程图调用下一步
    }

    public virtual string GetTip()
    {
        return "";
    }
}
