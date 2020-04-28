using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public static class CollectionsExtension
{
    /// <summary>
    /// 遍历对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ienumerable">遍历的对象</param>
    /// <param name="callback">做的事</param>
    public static void ForEach<T>(this IEnumerable<T> ienumerable,UnityAction<T> callback)
    {
        foreach (var i in ienumerable)
        {
            callback?.Invoke(i);
        }
    }

    /// <summary>
    /// 带索引的对象遍历
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="callback"></param>
    public static void For<T>(this T[] array, UnityAction<T, int> callback)
    {
        for (int i = 0; i < array.Count(); i++)
        {
            callback?.Invoke(array[i], i);
        }
    }

    /// <summary>
    /// 获取数组中的随机元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="iList"></param>
    /// <returns></returns>
    //public static T Random<T>(this T[] array)
    //{
    //    return array[UnityEngine.Random.Range(0, array.Length)];
    //}

    public static T Random<T>(this IList<T> iList)
    {
        return iList[UnityEngine.Random.Range(0, iList.Count)];
    }

    public static T[] Add<T>(this T[] array,T element)
    {
        T[] newArray = new T[array.Length+1];
        array.CopyTo(newArray,0);
        newArray[newArray.Length - 1] = element;
        return newArray;
    }

    public static T[] AddToHead<T>(this T[] array, T element)
    {
        T[] newArray = new T[array.Length + 1];
        array.CopyTo(newArray, 1);
        newArray[0] = element;
        return newArray;
    }

    public static T[] Add<T>(this T[] array, T[] elements)
    {
        T[] newArray = new T[array.Length + elements.Length];
        array.CopyTo(newArray,0);
        elements.CopyTo(newArray,array.Length);
        return newArray;
    }

    public static T[] Remove<T>(this T[] array, T element)
    {
        var list = array.ToList();
        list.Remove(element);
        return list.ToArray();
    }

    /// <summary>
    /// 数组是否为null或长度为0
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this Array array)
    {
        return array == null || array.Length == 0;
    }

    public static void Destroy<T>(this IEnumerable<T> ienumerable) where T:Object
    {
        foreach (var i in ienumerable)
        {
            Object.Destroy(i);
        }
    }

    public static void DestroyImmediate<T>(this IEnumerable<T> ienumerable) where T : Object
    {
        foreach (var i in ienumerable)
        {
            Object.DestroyImmediate(i);
        }
    }

    /// <summary>
    /// 拼接集合,以字符串输出
    /// </summary>
    /// <param name="iEnumerable"></param>
    /// <param name="jointChar"></param>
    /// <returns></returns>
    public static string Joint(this IEnumerable iEnumerable,char jointChar)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var i in iEnumerable)
        {
            if (sb.Length != 0)
                sb.Append(jointChar);
            sb.Append(i);
        }

        return sb.ToString();
    }

}
