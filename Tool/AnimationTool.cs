using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTool
{
    public static TimeDelay PlayForward(Animation animation, string clipName = "",UnityAction onComplete = null)
    {
        if (string.IsNullOrEmpty(clipName))
        {
            clipName = animation.clip.name;
        }
        animation[clipName].speed = 1;
        animation[clipName].normalizedTime = 0;
        animation.Play(clipName);

        return TimeDelay.Delay(animation[clipName].length, () =>
        {
            if (onComplete != null)
                onComplete();
        });
    }
    public static TimeDelay PlayBack(Animation animation, string clipName = "", UnityAction onComplete = null)
    {
        if (string.IsNullOrEmpty(clipName))
        {
            clipName = animation.clip.name;
        }
        animation[clipName].speed = -1;
        animation[clipName].normalizedTime = 1;
        animation.Play(clipName);

        return TimeDelay.Delay(animation[clipName].length, () =>
        {
            if (onComplete != null)
                onComplete();
        });
    }

    public static void Reset(Animation animation, string clipName = "")
    {
        if (string.IsNullOrEmpty(clipName))
            clipName = animation.clip.name;
        animation.Play(clipName);
        animation[clipName].time = 0;
        animation.Sample();
        animation.Stop();
    }

    public static void Complete(Animation animation, string clipName = "")
    {
        if (string.IsNullOrEmpty(clipName))
            clipName = animation.clip.name;
        animation[clipName].normalizedTime = 1;
    }
}
