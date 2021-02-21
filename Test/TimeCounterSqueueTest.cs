using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MyFrameworkPure;
class TimeCounterSqueueTest:MonoBehaviour
{
    void Start()
    {
        TimeCounterSqueue squeue = new TimeCounterSqueue();
        TimeCounter t1 = TimeCounter.CreateTimeCounter(1, null, () => Debug.Log(1));
        TimeCounter t2 = TimeCounter.CreateTimeCounter(0, null, () => Debug.Log(2));
        TimeCounter t3 = TimeCounter.CreateTimeCounter(1, null, () => Debug.Log(3));
        TimeCounter t4 = TimeCounter.CreateTimeCounter(1, null, () => Debug.Log(4));
        squeue.Add(t1, t2,t3,t4);
        squeue.Start();
    }
}
