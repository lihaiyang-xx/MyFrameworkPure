using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// debug 工具
    /// </summary>
    public class DebugTool
    {
        /// <summary>
        /// 打印子物体层级
        /// </summary>
        /// <param name="root"></param>
        public static void LogChildrenHierarchy(Transform root)
        {
            string result = string.Empty;
            PrintChildren(root,string.Empty,ref result);
            Debug.Log(result);
        }

        static void PrintChildren(Transform t, string indent,ref string result)
        {
            int childCount = t.childCount;
            result = $"{result}\n{indent}{t.name}";

            var moreIndent = indent + "\t";
            for (int i = 0; i < childCount; ++i)
            {
                var child = t.GetChild(i);
                PrintChildren(child, moreIndent,ref result);
            }
        }
    }
}

