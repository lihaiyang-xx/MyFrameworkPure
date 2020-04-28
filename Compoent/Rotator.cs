using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 pivot;

    [SerializeField] private Space relativeto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(pivot * speed * Time.deltaTime,relativeto);
    }
}
