using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public enum FacePivot
    {
        PositiveZ,
        NegativeZ
    }
    /// <summary>
    /// 控制物体朝向目标
    /// </summary>

    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private bool keepHorizontal;

        [SerializeField] private FacePivot facePivot;
        // Start is called before the first frame update
        void Start()
        {
            target = Camera.main?.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if(target == null)
                return;
            Vector3 dir = target.position - transform.position;
            if (keepHorizontal)
                dir.y = 0;
            if (dir == Vector3.zero)
                return;

            switch (facePivot)
            {
                case FacePivot.PositiveZ:
                    transform.forward = dir;
                    break;
                case FacePivot.NegativeZ:
                    transform.forward = -dir;
                    break;
            }
        }

        /// <summary>
        /// 目标变换
        /// </summary>
        public Transform Target
        {
            get => target;
            set => target = value;
        }

        /// <summary>
        /// 物体是否保持水平
        /// </summary>
        public bool KeepHorizontal
        {
            get => keepHorizontal;
            set => keepHorizontal = value;
        }

        /// <summary>
        /// 物体正朝向轴
        /// </summary>
        public FacePivot ForwardPivot
        {
            get => facePivot;
            set => facePivot = value;
        }
    }
}

