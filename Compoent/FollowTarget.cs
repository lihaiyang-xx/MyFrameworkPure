using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 1;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
    }
}
