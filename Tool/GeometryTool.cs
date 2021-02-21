using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryTool
{
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

    public static float GetPointToLineDistance(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 line = lineEnd - lineStart;
        Vector2 v = point - lineStart;
        return Vector3.Cross(line.normalized, v).magnitude;
    }
}
