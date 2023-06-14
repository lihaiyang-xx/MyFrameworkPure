using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 并行节点
    /// </summary>
    public class ParallelNode : StructNode
    {
        public override void Enter()
        {
            flowNodes.ForEach(x => x.Enter());
        }

        public override void Update()
        {
            flowNodes.ForEach(x => x.Update());
            //flowNodes.ForEach(x=>Debug.Log(x.GetType()+" "+x.Status));
            if (flowNodes.All(x => x.Status == NodeStatus.Complete))
            {
                Exit();
            }
        }

        public override void Complete()
        {

        }

        public override void Reset()
        {
            flowNodes.ForEach(x => x.Reset());
        }
    }
}

