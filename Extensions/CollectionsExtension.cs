using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace MyFrameworkPure
{
    /// <summary>
    /// 集合类拓展
    /// </summary>
    public static class CollectionsExtension
    {
        /// <summary>
        /// 遍历对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">遍历的对象</param>
        /// <param name="callback">做的事</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable,Action<T> callback)
        {
            foreach (var i in enumerable)
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
        public static T Random<T>(this IList<T> iList)
        {
            if (iList == null || iList.Count == 0)
            {
                throw new ArgumentException("List cannot be null or empty.");
            }

            return iList[UnityEngine.Random.Range(0, iList.Count)];
        }

        /// <summary>
        /// 添加一个元素到数组尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array,T element)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }

            int originalLength = array.Length;
            Array.Resize(ref array, originalLength + 1);
            array[originalLength] = element;

            return array;
        }

        /// <summary>
        /// 添加一个元素到数组头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] AddToHead<T>(this T[] array, T element)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }

            int originalLength = array.Length;
            Array.Resize(ref array, originalLength + 1);
            Array.Copy(array, 0, array, 1, originalLength);
            array[0] = element;

            return array;
        }

        /// <summary>
        /// 添加一个数组到数组尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, T[] elements)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Array cannot be null.");
            }

            if (elements == null || elements.Length == 0)
            {
                return array;
            }

            T[] newArray = new T[array.Length + elements.Length];
            Array.Copy(array, 0, newArray, 0, array.Length);
            Array.Copy(elements, 0, newArray, array.Length, elements.Length);

            return newArray;
        }

        /// <summary>
        /// 从数组移除一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] Remove<T>(this T[] array, T element)
        {
            int index = Array.IndexOf(array, element);
            if (index < 0)
            {
                return array; 
            }

            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
            Array.Resize(ref array, array.Length - 1);

            return array;
        }

        /// <summary>
        /// 数组是否为null或长度为0
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this IList list)
        {
            return list == null || list.Count == 0;
        }


        public static void Destroy<T>(this IList<T> list) where T : Object
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Object.Destroy(list[i]);
            }
        }

        /// <summary>
        /// 立刻删除整个迭代器中的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        public static void DestroyImmediate<T>(this IEnumerable<T> enumerable) where T : Object
        {
            foreach (var i in enumerable)
            {
                Object.DestroyImmediate(i);
            }
        }

        public static void DestroyImmediate<T>(this IEnumerable<T> enumerable,bool allowDestroyingAssets) where T : Object
        {
            foreach (var i in enumerable)
            {
                Object.DestroyImmediate(i,allowDestroyingAssets);
            }
        }

        /// <summary>
        /// 拼接集合,以字符串输出
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="jointChar"></param>
        /// <returns></returns>
        public static string JointToString(this IEnumerable enumerable,char jointChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in enumerable)
            {
                if (sb.Length != 0)
                    sb.Append(jointChar);
                sb.Append(i);
            }

            return sb.ToString();
        }

        public static void Log(this IEnumerable enumerable)
        {
            string result = "[";
            foreach (var item in enumerable)
            {
                result += item + ", ";
            }
            if (result.Length > 1)
            {
                result = result.Remove(result.Length - 2); // 移除最后的逗号和空格
            }
            result += "]";
            Debug.Log(result);
        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> RandomSort<T>(this IEnumerable<T> iEnumerable)
        {
            System.Random random = new System.Random();
            return iEnumerable.OrderBy(x => random.Next());
        }

        /// <summary>
        /// 获取元素的索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T element)
        {
            return Array.IndexOf(array, element);
        }

        /// <summary>
        /// 获取数组范围
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T[] GetRange<T>(this T[] array, int startIndex, int count)
        {
            T[] result = new T[count];
            Array.Copy(array, startIndex, result, 0, count);
            return result;
        }

        /// <summary>
        /// 将 IEnumerator 转换为 IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// ConcurrentBag清除方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        public static void Clear<T>(this ConcurrentBag<T> bag)
        {
            while (!bag.IsEmpty)
            {
                bag.TryTake(out T _);
            }
        }

        /// <summary>
        /// 获取二维数组的一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        /// <summary>
        /// 获取二维数组的一行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }
    }
}

