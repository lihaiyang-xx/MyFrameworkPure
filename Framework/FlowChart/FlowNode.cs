using System.Collections;
using System.Collections.Generic;
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
            if (Chart != null)
                Chart.ExecuteNext();//流程图调用下一步
        }

        public virtual void Reset()
        {

        }

        public virtual void Complete()
        {

        }

        public virtual string GetTip()
        {
            return "";
        }
    }
}

