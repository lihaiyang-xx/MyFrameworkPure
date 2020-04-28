using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AssemblyTool : MonoBehaviour
{
    public static Type GetType(string typeName)
    {
        Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        return assembly.GetType(typeName);
    }

    public static Type GetType(string typeName, string assemblyName)
    {
        Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
        return assembly.GetType(typeName);
    }

}
