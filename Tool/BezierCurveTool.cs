using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
    public static class BezierCurveTool
    {
        public delegate Vector3 GetBezierPoint3(Vector3 start, Vector3 control, Vector3 end, float t);

        public delegate Vector3 GetBezierPoint4(Vector3 start, Vector3 startControl, Vector3 endControl, Vector3 end,
            float t);

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


        public static Vector3 Bezier3(Vector3 start, Vector3 control, Vector3 end, float t)
        {
            return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
        }


        public static Vector3 Bezier4(Vector3 s, Vector3 st, Vector3 et, Vector3 e, float t)
        {
            return (((-s + 3 * (st - et) + e) * t + (3 * (s + et) - 6 * st)) * t + 3 * (st - s)) * t + s;
        }

    }

}

