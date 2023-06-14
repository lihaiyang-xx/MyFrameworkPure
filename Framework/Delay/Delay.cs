using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if DoTween
using DG.Tweening;
#endif

namespace MyFrameworkPure
{
    /// <summary>
    /// 延迟抽象类
    /// </summary>
    public abstract class Delay : IMonoUpdate
    {
        public UnityAction endCall;//延迟结束时调用

        public virtual bool Active { get; set; }//是否激活状态

        public Delay()
        {

        }

        public abstract void MonoUpdate();

        public abstract bool IsEnd();

        /// <summary>
        /// 销毁延迟
        /// </summary>
        public void Destroy()
        {
            Active = false;
            endCall = null;
            MonoBehaviorTool.Instance.UnRegisterUpdate(this);
        }

        public static Delay DoDelay(float _duration, UnityAction _endCall)
        {
            return new TimeDelay(_duration, null, _endCall);
        }

        public static Delay DelayUntil(ConditionDelay.Predicate _predicate, UnityAction _endCall)
        {
            return new ConditionDelay(_predicate, _endCall);
        }

    }

    /// <summary>
    /// 时间延迟
    /// </summary>
    public class TimeDelay : Delay
    {

        private float timeCounter;
        private float duration;
        private float frequency;
        private float frequencyCounter;

        private bool useFrame;

        private UnityAction updateCall;

        public TimeDelay(float _duration, UnityAction _updateCall, UnityAction _endCall, float _frequency = 0, bool _useFrame = false) : base()
        {
            duration = _duration;
            updateCall = _updateCall;
            endCall = _endCall;
            frequency = _frequency;
            useFrame = _useFrame;
            Active = true;
            MonoBehaviorTool.Instance.RegisterUpdate(this);
        }

        public override void MonoUpdate()
        {
            if (!Active)
                return;
            if (IsEnd())
            {
                if (endCall != null)
                    endCall();
                MonoBehaviorTool.Instance.UnRegisterUpdate(this);
            }
            else if (updateCall != null)
            {
                frequencyCounter += Time.deltaTime;
                if (frequencyCounter >= frequency)
                {
                    frequencyCounter = 0;
                    updateCall();
                }
            }
        }
        public override bool IsEnd()
        {
            timeCounter += useFrame ? 1 : Time.deltaTime;
            return timeCounter >= duration;
        }

        public void Stop()
        {
            Active = false;
            MonoBehaviorTool.Instance.UnRegisterUpdate(this);
        }

        public static TimeDelay Delay(float _duration, UnityAction _endCall)
        {
            return new TimeDelay(_duration, null, _endCall);
        }

        public static TimeDelay DelayFrame(float _duration, UnityAction _endCall)
        {
            return new TimeDelay(_duration, null, _endCall, 0, true);
        }
    }

    /// <summary>
    /// 条件延迟
    /// </summary>
    public class ConditionDelay : Delay
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

#if DoTween
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
                if (value)
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
            tweenAni.onComplete.AddListener(() => {
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
#endif

    
#if DoTween
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
                if (value)
                {
                    fadeObj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
                    CanvasGroup canvasGroup = fadeObj.GetComponent<CanvasGroup>();
                    DOTween.Kill(canvasGroup);
                    canvasGroup.alpha = 0;
                    canvasGroup.DOFade(1, 1.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        if (endCall != null)
                            endCall();
                    });
                }
                else
                {
                    CanvasGroup canvasGroup = fadeObj.GetComponent<CanvasGroup>();
                    DOTween.Kill(canvasGroup);
                }
                base.Active = value;
            }
        }

        public FadeinoutDelay(string _text, GameObject go)
        {
            fadeObj = go;
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
#endif

    /// <summary>
    /// 延迟队列
    /// </summary>
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
}


