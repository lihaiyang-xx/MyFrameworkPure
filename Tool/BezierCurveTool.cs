using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
    /// <summary>
    /// 贝塞尔曲线工具
    /// </summary>
    public static class BezierCurveTool
    {
        public delegate Vector3 GetBezierPoint3(Vector3 start, Vector3 control, Vector3 end, float t);

        public delegate Vector3 GetBezierPoint4(Vector3 start, Vector3 startControl, Vector3 endControl, Vector3 end,
            float t);

        /// <summary>
        /// 3点贝塞尔移动
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="start"></param>
        /// <param name="control"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        public static void DoBezier3Move(this Transform transform, Vector3 start, Vector3 control, Vector3 end,float duration, UnityAction onComplete = null)
        {
            if (control.Equals(Vector3.negativeInfinity))
            {
                Vector3 vect = end - start;
                control = start +  vect * 0.5f + Vector3.up * vect.magnitude * 0.5f;
            }

            float t = 0;
            TimeCounter.CreateTimeCounter(duration, () =>
            {
                t += Time.deltaTime/duration;
                Vector3 point = Bezier3(start, control, end, t);
                transform.position = point;
            }, ()=>
            {
                if (onComplete != null)
                    onComplete();
            }); 
        }

        /// <summary>
        /// 4点贝塞尔移动
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="start"></param>
        /// <param name="startControl"></param>
        /// <param name="endControl"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        public static void DoBezier4Move(this Transform transform, Vector3 start,
            Vector3 startControl, Vector3 endControl, Vector3 end, float duration)
        {
            float t = 0;
            TimeCounter.CreateTimeCounter(duration, () =>
            {
                t += Time.deltaTime / duration;
                Vector3 point = Bezier4(start, startControl,endControl, end, t);
                transform.position = point;
            }, null);
        }

        /// <summary>
        /// 3点贝塞尔公式
        /// </summary>
        /// <param name="start"></param>
        /// <param name="control"></param>
        /// <param name="end"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 Bezier3(Vector3 start, Vector3 control, Vector3 end, float t)
        {
            return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
        }

        /// <summary>
        /// 4点贝塞尔公式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="e"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 Bezier4(Vector3 s, Vector3 st, Vector3 et, Vector3 e, float t)
        {
            return (((-s + 3 * (st - et) + e) * t + (3 * (s + et) - 6 * st)) * t + 3 * (st - s)) * t + s;
        }

    }

}

