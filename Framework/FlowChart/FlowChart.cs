using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 流程图
/// </summary>
public class FlowChart : IMonoUpdate
{

    private readonly List<FlowNode> nodeList;//节点列表
    [HideInInspector]
    public int curIndex;

    public event UnityAction<int> OnStepChanged;
    public event UnityAction OnChartComplete;


    public FlowChart()
    {
        nodeList = new List<FlowNode>();
        curIndex = 0;
        MonoBehaviorTool.Instance.RegisterUpdate(this);
    }

    public int Count
    {
        get { return nodeList.Count; }
    }

    /// <summary>
    /// 开始执行流程图
    /// </summary>
    public void Start()
    {
        curIndex = 0;
        ExecuteStep(curIndex);
    }

    /// <summary>
    /// 执行下一步
    /// </summary>
    public void ExecuteNext()
    {
        ExecuteStep(curIndex+1);
    }

    public void ExecutePrevious()
    {
        ExecuteStep(curIndex-1);
    }

    public void ExecuteStep(int step)
    {
        if (step < 0 || step > nodeList.Count)
        {
            return;
        }
        else if(step == nodeList.Count)
        {
            Debug.Log("流程结束!");
            if (OnChartComplete != null) OnChartComplete();
        }
        else
        {
            int i = curIndex;
            int j = curIndex;
            while (i >= step)//往前跳步
            {
                nodeList[i].Reset();
                i--;
            }
            while (j < step)//往后跳步
            {
                nodeList[j].Complete();
                j++;
            }
            curIndex = step;
            //nodeList[curIndex].Reset();
            nodeList[curIndex].Enter();
            if (OnStepChanged != null) OnStepChanged(curIndex);
        }
    }

    //public void ExecuteStep(float process)
    //{
    //    int step = (int)(process * nodeList.Count);
    //    ExecuteStep(step);
    //}

    /// <summary>
    /// 将节点添加到节点列表中
    /// 方便流程控制
    /// </summary>
    /// <param equimpentName="node"></param>
    public void Add(FlowNode node)
    {
        nodeList.Add(node);
        node.Chart = this;
    }

    public void Clear()
    {
        nodeList.Clear();
        MonoBehaviorTool.Instance.UnRegisterUpdate(this);
    }

    /// <summary>
    /// 执行Update方法
    /// </summary>
    public void MonoUpdate()
    {
        if (curIndex < nodeList.Count)
        {
            nodeList[curIndex].Update();
        }
    }

    public string GetTip()
    {
        FlowNode node = nodeList[curIndex];
        if (node != null)
            return node.GetTip();
        return string.Empty;
    }
}
