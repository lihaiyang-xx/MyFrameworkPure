using System.Collections;
using System.Collections.Generic;
using HTC.UnityPlugin.Vive.ExCamConfigInterface;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventFloat : UnityEvent<float>
{
}
public class GestureTool : CSingletonMono<GestureTool>
{

    public float HThreshold { get; set; }//水平阈值

    public float VThreshold { get; set; }//垂直阈值

    public UnityEvent<float> onHorizontalMove = new UnityEventFloat();

    public UnityEvent<float> onVerticalMove = new UnityEventFloat();

    public float mouseSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0))
            return;
        float deltaX = Input.GetAxis("Mouse X") * mouseSpeed;
        float deltaY = Input.GetAxis("Mouse Y") * mouseSpeed;

        if(Input.touchCount > 0)
        {
            deltaX = Input.touches[0].deltaPosition.x;
            deltaY = Input.touches[0].deltaPosition.y;
        }

        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x <= 0)
        {
            deltaX = -mouseSpeed * 0.5f;
        }
        else if(mousePos.x >= Screen.width)
        {
            deltaX = mouseSpeed * 0.5f;
        }

        if (mousePos.y <= 0)
        {
            deltaY = -mouseSpeed * 0.5f;
        }
        else if(mousePos.y >= Screen.height)
        {
            deltaY = mouseSpeed * 0.5f;
        }

        if (Mathf.Abs(deltaX) > HThreshold)
        {
            onHorizontalMove?.Invoke(deltaX);
        }

        if (Mathf.Abs(deltaY) > VThreshold)
        {
            onVerticalMove?.Invoke(deltaY);
        }
    }
}
