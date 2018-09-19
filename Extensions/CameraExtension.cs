using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class CameraExtension
{
    public static float HorizontalFOV(this Camera cam)
    {
        var radAngle = cam.fieldOfView * Mathf.Deg2Rad;
        var radHFOV = 2 * Math.Atan(Mathf.Tan(radAngle / 2) * cam.aspect);
        var hFOV = Mathf.Rad2Deg * radHFOV;
        return (float)hFOV;
    }
}

