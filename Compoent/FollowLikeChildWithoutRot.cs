using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 像子物体一样跟随目标
/// </summary>
public class FollowLikeChildWithoutRot : MonoBehaviour
{
    private Vector3 relativePos;

    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        relativePos = target.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
            return;
        transform.position = target.TransformPoint(relativePos);
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
        }
    }
}
