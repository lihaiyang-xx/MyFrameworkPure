using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public class GeometryTool
    {
        /// <summary>
        /// 射线和球是否相交
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="sphere"></param>
        /// <returns></returns>
        public static float? Intersects(Ray ray, BoundingSphere sphere)
        {
            Vector3 difference = sphere.position - ray.origin;
            float differenceLengthSquared = Vector3.SqrMagnitude(difference);
            float sphereRadiusSquared = sphere.radius * sphere.radius;
            float distanceAlongRay;
            //if (differenceLengthSquared < sphereRadiusSquared)
            //{
            //    return 0.0f;
            //}
            Vector3 refDirection = ray.direction;
            distanceAlongRay = Vector3.Dot(refDirection, difference);
            if (distanceAlongRay < 0)
            {
                return null;
            }
            float dist = sphereRadiusSquared + distanceAlongRay * distanceAlongRay - differenceLengthSquared;
            return (dist < 0) ? null : distanceAlongRay - (float?)Mathf.Sqrt(dist);
        }

        /// <summary>
        /// 点到线段的距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        public static float GetPointToLineDistance(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 line = lineEnd - lineStart;
            Vector2 v = point - lineStart;
            return Vector3.Cross(line.normalized, v).magnitude;
        }
    }
}

