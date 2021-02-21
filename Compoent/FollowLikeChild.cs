using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 像子物体一样跟随目标
    /// </summary>
    public class FollowLikeChild : MonoBehaviour
    {
        private Vector3 relativePos;

        private Quaternion relativeRot;

        [SerializeField] private Transform target;
        // Start is called before the first frame update
        void Start()
        {
            relativePos = target.InverseTransformPoint(transform.position);
            relativeRot = Quaternion.Inverse(target.rotation) * transform.rotation;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (target == null)
                return;
            transform.position = target.TransformPoint(relativePos);
            transform.rotation = relativeRot * target.rotation;
        }

        /// <summary>
        /// 目标变换
        /// </summary>
        public Transform Target
        {
            get => target;
            set
            {
                target = value;
                relativePos = target.InverseTransformPoint(transform.position);
                relativeRot = Quaternion.Inverse(target.rotation) * transform.rotation;
            }
        }
    }
}

