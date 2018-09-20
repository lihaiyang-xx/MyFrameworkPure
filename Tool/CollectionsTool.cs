using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public class CollectionsTool
    {
        public static T[] Merge<T>(T[] arr, T[] other)
        {
            T[] buffer = new T[arr.Length + other.Length];
            arr.CopyTo(buffer, 0);
            other.CopyTo(buffer, arr.Length);
            return buffer;
        }

        public static T[] MergerArray<T>(params T[][] arrays)
        {
            List<T> byteList = new List<T>();
            foreach (var array in arrays)
            {
                byteList.AddRange(array);
            }

            return byteList.ToArray();
        }
    }
}

