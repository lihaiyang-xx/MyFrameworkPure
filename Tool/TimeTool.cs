using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 时间工具
    /// </summary>
    public static class TimeTool
    {
        /// <summary>
        /// 获取时间戳(秒)
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            long timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return timeStamp;
        }

        /// <summary>
        /// 获取13位时间戳(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp13()
        {
            long timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            return timeStamp;
        }

        /// <summary>
        /// 获取时间字符串
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStr()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy-MM-dd hh:mm:ss.f");
        }
    }
}

