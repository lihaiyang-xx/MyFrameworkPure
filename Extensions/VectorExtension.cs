using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 Set(this Vector3 v, char pivot, float value)
    {
        switch (pivot)
        {
            case 'x':
                v.x = value;
                break;
            case 'y':
                v.y = value;
                break;
            case 'z':
                v.z = value;
                break;
        }

        return v;
    }
}
