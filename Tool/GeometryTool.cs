using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public class GeometryTool
    {
        /// <summary>
        /// 三点贝塞尔曲线
        /// </summary>
        /// <param equimpentName="p0"></param>
        /// <param equimpentName="p1"></param>
        /// <param equimpentName="p2"></param>
        /// <param equimpentName="t"></param>
        /// <returns></returns>
        public static Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float t1 = (1 - t) * (1 - t);
            float t2 = t * (1 - t);
            float t3 = t * t;
            Vector3 b = p0 * t1 + 2 * t2 * p1 + t3 * p2;
            return b;
        }
    }

}

