using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrthographicRender : MonoBehaviour
{
    public Camera renderCamera;

    public float scale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if(!renderCamera)
            renderCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * GetHandleSize(renderCamera,transform.position) * scale;
    }

    public static float GetHandleSize(Camera camera, Vector3 position)
    {
        float result;
        if (camera)
        {
            Transform transform = camera.transform;
            Vector3 position2 = transform.position;
            float z = Vector3.Dot(position - position2, transform.TransformDirection(new Vector3(0f, 0f, 1f)));
            Vector3 a = camera.WorldToScreenPoint(position2 + transform.TransformDirection(new Vector3(0f, 0f, z)));
            Vector3 b = camera.WorldToScreenPoint(position2 + transform.TransformDirection(new Vector3(1f, 0f, z)));
            float magnitude = (a - b).magnitude;
            result = 80f / Mathf.Max(magnitude, 0.0001f) ;
        }
        else
        {
            result = 20f;
        }
        return result;
    }

}
