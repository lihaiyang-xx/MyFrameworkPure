using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitConverterTool
{

    public static byte ToByte(byte[] bytes, ref int offset)
    {
        byte result = bytes[offset];
        offset += 1;
        return result;
    }

    public static Int16 ToInt16(byte[] bytes, ref int offset)
    {
        Int16 result = BitConverter.ToInt16(bytes, offset);
        offset += 2;
        return result;
    }

    public static int ToInt32(byte[] bytes, ref int offset)
    {
        int result = BitConverter.ToInt32(bytes, offset);
        offset += 4;
        return result;
    }
    public static Int64 ToInt64(byte[] bytes, ref int offset)
    {
        Int64 result = BitConverter.ToInt64(bytes, offset);
        offset += 8;
        return result;
    }

    public static float ToSingle(byte[] bytes, ref int offset)
    {
        float result = BitConverter.ToSingle(bytes, offset);
        offset += 4;
        return result;
    }

    public static double ToDouble(byte[] bytes, ref int offset)
    {
        double result = BitConverter.ToDouble(bytes, offset);
        offset += 8;
        return result;
    }

    public static bool ToBoolean(byte[] bytes, ref int offset)
    {
        bool result = BitConverter.ToBoolean(bytes, offset);
        offset += 1;
        return result;
    }
}
