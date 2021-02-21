using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyFrameworkPure
{
    /// <summary>
    /// 内存工具
    /// </summary>
    public static class MemoryTool
    {
        /// <summary>
        /// 清理内存
        /// </summary>
        public static void ClearMemory()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}

