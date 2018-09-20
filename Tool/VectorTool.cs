using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class VectorTool
    {
        public static Vector3 ConvertStringToVecotr3(string str, char c = ',')
        {
            try
            {
                string[] splits = str.Split(c);
                return new Vector3(float.Parse(splits[0]), float.Parse(splits[1]), float.Parse(splits[2]));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return Vector3.zero;
        }
    }
}

