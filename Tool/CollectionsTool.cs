using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static T[] GetRandomArray<T>(T[] array,int length)
        {
            if (array.Length < length)
                return null;
            List<T> temp = new List<T>(array);
            T[] finalArray = new T[length];
            for(int i = 0;i< finalArray.Length;i++)
            {
                int randomValue = Random.Range(0, temp.Count);
                finalArray[i] = temp[randomValue];
                temp.RemoveAt(randomValue);
            }
            return finalArray;
        }

        public static bool EqualArray<T>(T[] first, T[] second)
        {
            return first.SequenceEqual(second);
        }
    }
}

