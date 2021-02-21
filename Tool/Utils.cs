using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 其他工具类
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 生成uid
        /// </summary>
        /// <returns></returns>
        public static string GenerateUID()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
