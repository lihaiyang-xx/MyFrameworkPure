using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class FlowNode
    {
        public FlowChart Chart { get; set; }//流程图
        public string Name { get; private set; }

        public NodeStatus Status { get; private set; }

        public FlowNode(string name)
        {
            Name = name;
        }

        public FlowNode()
        {

        }

        /// <summary>
        /// 进入执行
        /// </summary>
        public virtual void Enter()
        {
            Status = NodeStatus.Enter;
        }

        /// <summary>
        /// 每帧更新
        /// </summary>
        public virtual void Update()
        {
            if(Status < NodeStatus.Update)
                Status = NodeStatus.Update;
        }
        /// <summary>
        /// 退出执行
        /// </summary>
        public void Exit()
        {
            Complete();
            if (Chart != null)
                Chart.ExecuteNext();//流程图调用下一步
        }

        public virtual void Reset()
        {
            Status = NodeStatus.Reset;
        }

        public virtual void Complete()
        {
            Status = NodeStatus.Complete;
        }

        public virtual string GetTip()
        {
            return "";
        }
    }
}

public enum NodeStatus
{
    None,
    Enter,
    Update,
    Complete,
    Reset
}

