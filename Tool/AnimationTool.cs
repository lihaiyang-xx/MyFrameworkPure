using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTool
{
    public static void PlayForward(Animation animation, string clipName)
    {
        animation[clipName].speed = 1;
        animation[clipName].normalizedTime = 0;
        animation.Play();
    }
    public static void PlayBack(Animation animation, string clipName)
    {
        animation[clipName].speed = -1;
        animation[clipName].normalizedTime = 1;
        animation.Play();
    }

    public static void Reset(Animation animation, string clipName)
    {
        animation[clipName].normalizedTime = 0;
    }
}
