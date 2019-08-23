using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public static class CollectionsExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ienumerable">遍历的对象</param>
    /// <param name="callback">做的事</param>
    public static void ForEach<T>(this IEnumerable<T> ienumerable,UnityAction<T> callback)
    {
        foreach (var i in ienumerable)
        {
            if (callback != null) callback(i);
        }
    }

    public static void For<T>(this T[] array, UnityAction<T, int> callback)
    {
        for (int i = 0; i < array.Count(); i++)
        {
            if (callback != null)
                callback(array[i], i);
        }
    }

    public static T Random<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static void Destroy<T>(this IEnumerable<T> ienumerable) where T:Object
    {
        foreach (var i in ienumerable)
        {
            Object.Destroy(i);
        }
    }
	
}
