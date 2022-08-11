using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
    /// <summary>
    /// 动画工具类
    /// </summary>
    public class AnimationTool
    {
        /// <summary>
        /// 正向播放动画
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="clipName"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public static Delay PlayForward(Animation animation, string clipName = "", UnityAction onComplete = null)
        {
            if (string.IsNullOrEmpty(clipName))
            {
                clipName = animation.clip.name;
            }
            animation[clipName].speed = 1;
            animation[clipName].normalizedTime = 0;
            animation.Play(clipName);

            //return Delay.DelayUntil(()=>animation[clipName].normalizedTime>=1, () =>
            //{
            //    onComplete?.Invoke();
            //});
            return TimeDelay.Delay(animation[clipName].length, () =>
            {
                if (onComplete != null)
                    onComplete();
            });
        }

        /// <summary>
        /// 倒播动画
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="clipName"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public static Delay PlayBack(Animation animation, string clipName = "", UnityAction onComplete = null)
        {
            if (string.IsNullOrEmpty(clipName))
            {
                clipName = animation.clip.name;
            }
            animation[clipName].speed = -1;
            animation[clipName].normalizedTime = 1;
            animation.Play(clipName);

            //return Delay.DelayUntil(() => animation[clipName].normalizedTime >= 1, () =>
            //{
            //    onComplete?.Invoke();
            //});
            return TimeDelay.Delay(animation[clipName].length, () =>
            {
                if (onComplete != null)
                    onComplete();
            });
        }

        /// <summary>
        /// 重置动画到第一帧
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="clipName"></param>
        public static void Reset(Animation animation, string clipName = "")
        {
            if (string.IsNullOrEmpty(clipName))
                clipName = animation.clip.name;
            animation.Play(clipName);
            animation[clipName].time = 0;
            animation.Sample();
            animation.Stop();
        }

        /// <summary>
        /// 重置动画到最后一帧
        /// </summary>
        /// <param name="animation"></param>
        /// <param name="clipName"></param>
        public static void Complete(Animation animation, string clipName = "")
        {
            if (string.IsNullOrEmpty(clipName))
                clipName = animation.clip.name;
            animation.Play(clipName);
            animation[clipName].normalizedTime = 1;
            animation.Sample();
            animation.Stop();
        }

        /// <summary>
        /// 重置动画到最后一帧(Animator)
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="clipName"></param>
        public static void Complete(Animator animator, string clipName)
        {
            //AnimationClip clip = animator.runtimeAnimatorController.animationClips.First(x => x.name == clipName);
            animator.Play(clipName, 0, 1);
        }
    }
}

