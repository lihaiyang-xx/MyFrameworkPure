using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 程序集工具类
    /// </summary>
    public class AssemblyTool : MonoBehaviour
    {
        /// <summary>
        /// 根据类型名和当前程序集获取类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetType(string typeName)
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return assembly.GetType(typeName);
        }

        /// <summary>
        /// 根据类型名和程序集名称获取类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Type GetType(string typeName, string assemblyName)
        {
            Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
            return assembly.GetType(typeName);
        }

    }
}

