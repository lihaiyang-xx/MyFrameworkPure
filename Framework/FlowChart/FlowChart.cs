using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 流程图
/// </summary>
public class FlowChart : IMonoUpdate
{

    private readonly List<FlowNode> nodeList;//节点列表

    private int curIndex;

    public FlowChart()
    {
        nodeList = new List<FlowNode>();
        curIndex = 0;
        MonoBehaviorTool.RegisterUpdate(this);
    }

    /// <summary>
    /// 开始执行流程图
    /// </summary>
    public void Start()
    {
        curIndex = 0;
        if (curIndex < nodeList.Count)
            nodeList[curIndex].Enter();
        else
        {
            Debug.Log("图中没有任何节点");
        }
    }

    /// <summary>
    /// 执行下一步
    /// </summary>
    public void ExecuteNext()
    {
        curIndex++;
        if (curIndex < nodeList.Count)
            nodeList[curIndex].Enter();
        else
        {
            Debug.Log("图中没有后续节点");
        }
    }

    public void ExecuteStep(int step)
    {
        curIndex = step;
        if (curIndex < nodeList.Count)
        {
            nodeList[curIndex].Enter();
            curIndex = 0;
        }

        else
        {
            Debug.Log("结点不存在：" + step);
        }
    }

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
        MonoBehaviorTool.UnRegisterUpdate(this);
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
