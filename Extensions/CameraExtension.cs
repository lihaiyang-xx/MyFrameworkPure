using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class CameraExtension
{
    public static float HorizontalFov(this Camera cam)
    {
        var radAngle = cam.fieldOfView * Mathf.Deg2Rad;
        var radHfov = 2 * Math.Atan(Mathf.Tan(radAngle / 2) * cam.aspect);
        var hFov = Mathf.Rad2Deg * radHfov;
        return (float)hFov;
    }

   
}

