using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class ColorTool
    {
        public static byte[] Color2Bytes(Color color)
        {
            return Color2Bytes(color.r, color.g, color.b, color.a);
        }

        public static byte[] Color2Bytes(float r, float g, float b, float a)
        {
            byte[] rBytes = System.BitConverter.GetBytes(r);
            byte[] gBytes = System.BitConverter.GetBytes(g);
            byte[] bBytes = System.BitConverter.GetBytes(b);
            byte[] aBytes = System.BitConverter.GetBytes(a);

            System.Collections.Generic.List<byte> byteList = new List<byte>();
            byteList.AddRange(rBytes);
            byteList.AddRange(gBytes);
            byteList.AddRange(bBytes);
            byteList.AddRange(aBytes);

            return byteList.ToArray();

        }

        public static Color Bytes2Color(byte[] bytes)
        {
            Color color = Color.black;
            for (int i = 0; i < 4; i++)
            {
                float value = System.BitConverter.ToSingle(bytes, i * 4);
                color[i] = value;
            }
            return color;
        }

        public static Vector3 ConvertColorToVector3(Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }

        public static Color ConvertVector3ToColor(Vector3 vector)
        {
            return new Color(vector.x, vector.y, vector.z);
        }
    }
}

