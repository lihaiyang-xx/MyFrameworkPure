using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    public class StructNode : FlowNode
    {
        protected List<FlowNode> flowNodes = new List<FlowNode>();
        
        public bool IsEnd { get; set; }
        public void AddNodes(FlowNode flowNode)
        {
            flowNodes.Add(flowNode);
        }

        public static StructNode CreateFromName(string name)
        {
            switch (name)
            {
                case "@开始并行@":
                    return new ParallelNode(){IsEnd = false};
                case "@结束并行@":
                    return new ParallelNode(){IsEnd = true};
            }
            return null;
        }
    }
}

