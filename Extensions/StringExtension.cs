using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class StringExtension
    {
        /// <summary>
        /// 获取字符串后缀
        /// </summary>
        /// <param name="source"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetPostfix(this string source, char flag)
        {
            int position = source.LastIndexOf(flag);
            return source.Remove(0, position + 1);
        }

        /// <summary>
        /// 字符串编码转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="dstEncoding"></param>
        /// <returns></returns>
        public static string EncodeConvert(this string source, Encoding srcEncoding, Encoding dstEncoding)
        {
            byte[] bytes = srcEncoding.GetBytes(source);
            return dstEncoding.GetString(bytes);
        }

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        public static bool IsNumber(this string s)
        {
            Regex regex = new Regex("[^0-9]");
            return !regex.IsMatch(s);
        }

        /// <summary>
        /// 将字符串转换为整形数组
        /// </summary>
        /// <param name="s"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static int[] ConvertToInts(this string s, char splitChar = ',')
        {
            string[] splits = s.Split(splitChar);
            int[] result = new int[splits.Length];
            for (int i = 0; i < splits.Length; i++)
            {
                result[i] = int.Parse(splits[i]);
            }

            return result;
        }

        /// <summary>
        /// 字符串是否包含任意数组中的元素
        /// </summary>
        /// <param name="s"></param>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static bool Contains(this string s, string[] strs)
        {
            foreach (var str in strs)
            {
                if (s.Contains(str))
                    return true;
            }
            return false;
        }

        static Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);
        /// <summary>
        /// Unicode转义解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnicodeDecode(this string s)
        {
            return reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
        }
    }
}

