using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeCounter : MonoBehaviour
{
    private float counter;
    private float duration;
    private UnityAction updateCall;
    public  UnityAction endCall;

    public static TimeCounter CreateTimeCounter(float duration,UnityAction updateCall,UnityAction endCall)
    {
        GameObject go = new GameObject("TimeCounter");
        TimeCounter timeCounter = go.AddComponent<TimeCounter>();
        timeCounter.duration = duration;
        timeCounter.updateCall = updateCall;
        timeCounter.endCall = endCall;
        return timeCounter;
    }

    public static void StopAll()
    {
        TimeCounter[] counters = GameObject.FindObjectsOfType<TimeCounter>();
        counters.ForEach(x=>x.Stop());
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    counter += Time.deltaTime;
	    if (counter > duration)
	    {
	        if (endCall != null) endCall();
            Destroy(gameObject);
	    }
	    else
	    {
	        if (updateCall != null) updateCall();
	    }
	}

    public void Stop()
    {
        Destroy(gameObject);
    }
}

public class TimeCounterSqueue
{
    public UnityAction onComplete;
    private Queue<TimeCounter> queue = new Queue<TimeCounter>();
    public void Add(params TimeCounter[] timeCounters)
    {
        foreach (var timeCounter in timeCounters)
        {
            timeCounter.gameObject.SetActive(false);
            timeCounter.endCall += ExecuteNext;
            queue.Enqueue(timeCounter);
        }
    }

    public void Start()
    {
        ExecuteNext();
    }

    void ExecuteNext()
    {
        if (queue.Count == 0)
        {
            if (onComplete != null)
                onComplete();
            return;
        }
        TimeCounter timeCounter = queue.Dequeue();
        timeCounter.gameObject.SetActive(true);
    }

    public static void StopAll()
    {
        TimeCounter.StopAll();
    }
}
