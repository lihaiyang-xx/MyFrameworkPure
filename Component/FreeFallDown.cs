using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
    /// <summary>
    /// 自由落体组件(h=1/2gt²)
    /// </summary>
    public class FreeFallDown : MonoBehaviour
    {
        private float timeCounter = 0;

        private Vector3 originPos;

        private float gravity;

        public UnityAction onFallToGround;
        // Start is called before the first frame update
        void OnEnable()
        {
            originPos = transform.position;
            gravity = Physics.gravity.y;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            timeCounter += Time.fixedDeltaTime;
            float height = originPos.y + 0.5f * gravity * timeCounter * timeCounter;
            transform.position = originPos.Set('y', height);

            if (height < 0)
            {
                onFallToGround?.Invoke();
            }
        }
    }
}

