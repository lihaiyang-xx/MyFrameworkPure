﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 向量工具类
    /// </summary>
    public static class VectorTool
    {
        /// <summary>
        /// 将英文逗号分隔的字符串转换为Vector3
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 计算两个向量之间的角度(0-360)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static float Angle360(Vector3 from, Vector3 to,Vector3 axis)
        {
            float angle = Vector3.SignedAngle(from, to, axis);
            return angle < 0 ? 360 + angle : angle;
        }

        /// <summary>
        /// 将欧拉角分量默认范围0-360转为-180~180
        /// </summary>
        /// <param name="euler"></param>
        /// <returns></returns>
        public static Vector3 ConvertToEuler180(Vector3 euler)
        {
            if (euler.x > 180)
                euler.x = euler.x - 360;
            if (euler.y > 180)
                euler.y = euler.y - 360;
            if (euler.z > 180)
                euler.z = euler.z - 360;
            return euler;
        }
    }
}

