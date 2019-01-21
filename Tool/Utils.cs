using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void DelayDo(float delayTime, Action action)
    {
        new GameObject("delay").AddComponent<DelayMono>().Set(delayTime, action);
    }

}

public class DelayMono : MonoBehaviour
{
    public Action action;
    public float delayTime = 9999;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);
        if (action != null)
        {
            action();
        }
        Destroy(gameObject);
    }
    public void Set(float delayTime, Action action)
    {
        this.delayTime = delayTime;
        this.action = action;
    }
}