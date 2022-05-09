#if AsyncAwaitUtil
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityAsyncAwaitUtil;
using UnityEngine;
using UnityEngine.Events;

public class UnityAsyncTool
{
    public static async void DoUnityThread(UnityAction action)
    {
        await new WaitForUpdate();
        try
        {
            action?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        while (true)
        {
            await new WaitForBackgroundThread();
            if(!IsUnityThread())
                break;
        }
    }

    public static bool IsUnityThread()
    {
        return Thread.CurrentThread.ManagedThreadId == SyncContextUtil.UnityThreadId;
    }
}
#endif