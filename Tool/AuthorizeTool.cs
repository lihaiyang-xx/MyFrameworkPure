using System;
using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 授权工具
    /// </summary>
    public class AuthorizeTool : MonoBehaviour
    {
        public static bool IsValid(string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            long timeStamp = (new DateTimeOffset(dateTime)).ToUnixTimeSeconds();
            return TimeTool.GetTimeStamp() < timeStamp;
        }
    }
}

