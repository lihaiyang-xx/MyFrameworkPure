using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 1;

    [SerializeField] private bool attach = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        FollowQuick();
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
            return;
        transform.position = Vector3.Lerp(transform.position, target.position,attach?1:Time.deltaTime * speed);

        if (attach)
        {
            transform.rotation = target.rotation;
        }
    }

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public bool IsAttach
    {
        get => attach;
        set => attach = value;
    }

    public void FollowQuick()
    {
        if (target)
            transform.position = target.position;
    }
}
