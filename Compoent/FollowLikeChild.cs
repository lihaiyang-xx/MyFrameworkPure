using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 像子物体一样跟随目标
/// </summary>
public class FollowLikeChild : MonoBehaviour
{
    private Vector3 relativePos;

    private Quaternion relativeRot;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Target == null)
            return;
        transform.position = target.TransformPoint(relativePos);
        transform.rotation = relativeRot * target.rotation;
    }

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
