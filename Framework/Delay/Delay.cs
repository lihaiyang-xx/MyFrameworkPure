using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public abstract class Delay : IMonoUpdate
{
    public UnityAction endCall;

    public virtual bool Active { get; set; }

    public Delay()
    {
        
    }

    public abstract void MonoUpdate();

    public abstract bool IsEnd();

    public void Destroy()
    {
        Active = false;
        endCall = null;
        MonoBehaviorTool.Instance.UnRegisterUpdate(this);
    }

}

public class TimeDelay : Delay
{
    private float counter;
    private float duration;
    private float frequency;
    private float frequencyCounter;

    private UnityAction updateCall;

    public TimeDelay(float _duration, UnityAction _updateCall, UnityAction _endCall,float _frequency = 0) : base()
    {
        duration = _duration;
        updateCall = _updateCall;
        endCall = _endCall;
        frequency = _frequency;

        Active = true;
        MonoBehaviorTool.Instance.RegisterUpdate(this);
    }

    public override void MonoUpdate()
    {
        if (!Active)
            return;
        if (IsEnd())
        {
            if(endCall != null)
                endCall();
            MonoBehaviorTool.Instance.UnRegisterUpdate(this);
        }
        else if (updateCall != null)
        {
            frequencyCounter += Time.deltaTime;
            if(frequencyCounter >= frequency)
            {
                frequencyCounter = 0;
                updateCall();
            }
        }
    }
    public override bool IsEnd()
    {
        counter += Time.deltaTime;
        return counter >= duration;
    }
}

public class ConditionDelay:Delay
{
    public delegate bool Predicate();
    public Predicate predicate;
    
    public ConditionDelay(Predicate _predicate, UnityAction _endCall)
    {
        endCall = _endCall;
        predicate = _predicate;

        Active = true;
        MonoBehaviorTool.Instance.RegisterUpdate(this);
    }
    public override void MonoUpdate()
    {
        if (!Active)
            return;
        if (predicate())
        {
            if (endCall != null)
                endCall();
            MonoBehaviorTool.Instance.UnRegisterUpdate(this);
        }

    }
    public override bool IsEnd()
    {
        return true;
    }
}

public class DotweenDelay : Delay
{
    private DOTweenAnimation tweenAni;

    public override bool Active
    {
        get
        {
            return base.Active;
        }

        set
        {
            if(value)
            {
                tweenAni.DOPlay();
            }
            else
            {
                tweenAni.DOPause();
            }
            base.Active = value;
        }
    }
    public DotweenDelay(DOTweenAnimation _tweenAni)
    {
        tweenAni = _tweenAni;
        tweenAni.onComplete.AddListener(()=> {
            if (endCall != null) endCall();
        });
        
    }
    public override bool IsEnd()
    {
        return true;
    }

    public override void MonoUpdate()
    {
        
    }
}

public class FadeinoutDelay : Delay
{
    private string text;

    private GameObject fadeObj;

    public override bool Active
    {
        get
        {
            return base.Active;
        }

        set
        {
            DOTweenAnimation tweenAni = fadeObj.GetComponent<DOTweenAnimation>();

            if (value)
            {
                fadeObj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
                tweenAni.DORestart();
                fadeObj.transform.GetChild(0).GetComponent<DOTweenAnimation>().DORestart();
                tweenAni.onComplete.RemoveAllListeners();
                tweenAni.onComplete.AddListener(() => {
                    if (endCall != null) endCall();
                });
            }
            else
            {
                tweenAni.DOPause();
                fadeObj.transform.GetChild(0).GetComponent<DOTweenAnimation>().DOPause();
            }
            base.Active = value;
        }
    }

    public FadeinoutDelay(string _text)
    {
        fadeObj = SceneObjectManager.Instance.fadeinout;
        text = _text;
    }

    public override bool IsEnd()
    {
        return true;
    }

    public override void MonoUpdate()
    {
        
    }
}

public class DelaySqueue
{
    private Queue<Delay> queue = new Queue<Delay>();

    private Delay activeDelay;
    public void Add(params Delay[] delays)
    {
        foreach (var delay in delays)
        {
            delay.Active = false;
            delay.endCall += ExecuteNext;
            queue.Enqueue(delay);
        }
    }

    public void Start()
    {
        ExecuteNext();
    }

    public void Stop()
    {
        foreach (Delay delay in queue)
        {
            delay.Destroy();
        }
        activeDelay.Destroy();
        queue.Clear();
    }

    void ExecuteNext()
    {
        if (queue.Count == 0)
            return;
        Delay delay = queue.Dequeue();
        delay.Active = true;
        activeDelay = delay;
    }
}

