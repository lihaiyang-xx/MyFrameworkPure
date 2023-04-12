using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 像子物体一样跟随目标
    /// 要确保上层物体缩放是等比的,否则旋转时可能会发生缩放扭曲
    /// </summary>
    public class FollowLikeChild : MonoBehaviour
    {
        private Vector3 relativePos;

        private Quaternion relativeRot;

        [SerializeField] private Transform target;

        [SerializeField] private bool tween = false;
        [SerializeField] private float rotSpeed = 90;
        [SerializeField] private float moveSpeed = 1;

        [SerializeField] private bool useDefaultRelativePos = false;
        [SerializeField] private Vector3 defaultRelativePos;
        [SerializeField] private bool useDefalutRelativeRot = false;
        [SerializeField] private Quaternion defaultRelativeRot;
        // Start is called before the first frame update
        void Start()
        {
            if (target != null)
            {
                Target = target;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (target == null)
                return;
            Vector3 pos = target.TransformPoint(relativePos);
            Quaternion rot = relativeRot * target.rotation;

            transform.position = tween ? Vector3.Lerp(transform.position, pos, Time.deltaTime * moveSpeed) : pos;
            transform.rotation = tween ? Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed) : rot;
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
                relativePos =useDefaultRelativePos?defaultRelativePos:target.InverseTransformPoint(transform.position);
                relativeRot =useDefalutRelativeRot?defaultRelativeRot:Quaternion.Inverse(target.rotation) * transform.rotation;
            }
        }
    }
}

