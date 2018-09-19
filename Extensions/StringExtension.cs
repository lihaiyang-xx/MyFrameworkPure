using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringExtension
{
    /// <summary>
    /// 获取字符串前缀
    /// </summary>
    /// <param name="source">原字符串</param>
    /// <param name="flag">前缀标识符</param>
    /// <returns>前缀字符串</returns>
    public static string GetPostfix(this string source, char flag)
    {
        int position = source.LastIndexOf(flag);
        return source.Remove(0, position + 1);
    }

    /// <summary>
    /// 字符串编码转换
    /// </summary>
    /// <param name="source">字符串</param>
    /// <param name="srcEncoding">原始编码</param>
    /// <param name="dstEncoding">目标编码</param>
    /// <returns></returns>
    public static string EncodeConvert(this string source, Encoding srcEncoding,Encoding dstEncoding)
    {
        byte[] bytes = srcEncoding.GetBytes(source);
        return dstEncoding.GetString(bytes);
    }

    /// <summary>
    /// 是否为数字
    /// </summary>
    public static bool IsNumber(this string s)
    {
        Regex regex = new Regex("[^0-9]");
        return !regex.IsMatch(s);
    }
}
