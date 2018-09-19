using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsTool
{
    public static T[] Merge<T>(T[] arr, T[] other)
    {
        T[] buffer = new T[arr.Length + other.Length];
        arr.CopyTo(buffer, 0);
        other.CopyTo(buffer, arr.Length);
        return buffer;
    }
}
