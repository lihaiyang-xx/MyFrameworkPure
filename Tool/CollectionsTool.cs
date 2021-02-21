using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 集合工具类
    /// </summary>
    public class CollectionsTool
    {
        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T[] Merge<T>(T[] arr, T[] other)
        {
            T[] buffer = new T[arr.Length + other.Length];
            arr.CopyTo(buffer, 0);
            other.CopyTo(buffer, arr.Length);
            return buffer;
        }

        /// <summary>
        /// 合并多个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static T[] MergerArray<T>(params T[][] arrays)
        {
            List<T> byteList = new List<T>();
            foreach (var array in arrays)
            {
                byteList.AddRange(array);
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// 从数组中随机取出若干个元素,重新组成数组;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="length"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 比较两个数组是否相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool EqualArray<T>(T[] first, T[] second)
        {
            return first.SequenceEqual(second);
        }
    }
}

