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

        public static float Angle360(Vector3 from, Vector3 to,Vector3 axis)
        {
            float angle = Vector3.SignedAngle(from, to, axis);
            return angle < 0 ? 360 + angle : angle;
        }
    }
}

