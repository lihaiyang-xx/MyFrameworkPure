﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
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

        public static FlowChart ActiveFlowChart;


        public FlowChart()
        {
            nodeList = new List<FlowNode>();
            curIndex = -1;
            //MonoBehaviorTool.Instance.RegisterUpdate(this);
            MonoBehaviorTool.GetInstanceInActiveScene().RegisterUpdate(this);
        }

        public int Count
        {
            get { return nodeList.Count; }
        }

        public FlowNode CurrentNode
        {
            get
            {
                if (curIndex >= 0 && curIndex < nodeList.Count)
                    return nodeList[curIndex];
                return null;
            }
        }

        /// <summary>
        /// 开始执行流程图
        /// </summary>
        public void Start()
        {
            ActiveFlowChart = this;
            ExecuteStep(0);
        }

        /// <summary>
        /// 执行下一步
        /// </summary>
        public void ExecuteNext()
        {
            ExecuteStep(curIndex + 1);
        }

        /// <summary>
        /// 执行上一步
        /// </summary>
        public void ExecutePrevious()
        {
            ExecuteStep(curIndex - 1);
        }

        /// <summary>
        /// 执行步骤
        /// </summary>
        /// <param name="name"></param>
        public void ExecuteStep(string name)
        {
            int step = nodeList.IndexOf(nodeList.Find(x => x.Name == name));
            if (step == -1)
            {
                Debug.Log(name + "节点不存在!");
                return;
            }
            ExecuteStep(step);
        }

        /// <summary>
        /// 获取特定索引的步骤名称
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public string IndexToName(int step)
        {
            //int index = nodeList[step].Name.IndexOf("@");
            //return index < 0 ? nodeList[step].Name : nodeList[step].Name.Remove(index);
            return nodeList[step].Name;
        }

        /// <summary>
        /// 执行特定步骤
        /// </summary>
        /// <param name="step"></param>
        public void ExecuteStep(int step)
        {
            if (step < 0 || step > nodeList.Count)
            {
                return;
            }
            else if (step == nodeList.Count)
            {
                Debug.Log("流程结束!");
                nodeList[step - 1].Complete();
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
                while (j < step - 1)//往后跳步
                {
                    nodeList[j].Complete();
                    j++;
                }
                curIndex = step;

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

        /// <summary>
        /// 清除所有节点
        /// </summary>
        public void Clear()
        {
            nodeList.Clear();
            MonoBehaviorTool.GetInstanceInActiveScene().UnRegisterUpdate(this);
        }

        /// <summary>
        /// 执行Update方法
        /// </summary>
        public void MonoUpdate()
        {
            if (curIndex >=0 && curIndex < nodeList.Count)
            {
                nodeList[curIndex].Update();
            }
        }

        /// <summary>
        /// 获取节点备注
        /// </summary>
        /// <returns></returns>
        public string GetTip()
        {
            FlowNode node = nodeList[curIndex];
            if (node != null)
                return node.GetTip();
            return string.Empty;
        }

        /// <summary>
        /// 完成当前步骤
        /// </summary>
        public void CompleteCurrentStep()
        {
            if(curIndex < 0 || curIndex >= nodeList.Count)
                return;
            nodeList[curIndex]?.Complete();
        }
    }
}

