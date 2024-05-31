using UnityEngine;
using System.Collections;
using MyFrameworkPure;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 自由相机控制器
/// </summary>
public class FlyCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float damping = 0.05F;//插值速度

    [Header("旋转设置")]
    [SerializeField]
    private bool canRote = true;//可以旋转
    [SerializeField]
    private PointerEventData.InputButton rotBtn;
    [SerializeField]
    private float xSpeed = 80.0F;//水平旋转速度
    [SerializeField]
    private float ySpeed = 50.0F;//摄像机竖直旋转速度
    [SerializeField]
    private float yMinLimit = 10.0F;//竖直方向旋转限制最小值
    [SerializeField]
    private float yMaxLimit = 45.0F;//竖直方向旋转限制最大值

    [Header("拖拽设置")]
    [SerializeField]
    private bool canDrag = true;
    [SerializeField]
    private PointerEventData.InputButton dragBtn;

    [Header("缩放设置")]
    [SerializeField]
    private bool canScale = true;
    [SerializeField]
    private float mSpeed = 50.0F;//滚轴拉近速度
    [SerializeField]
    public float distance;//当前摄像机距离
    [SerializeField]
    private float minDistance = 0.0F;//摄像机最小距离
    [SerializeField]
    private float maxDistance = 230.0F;//摄像机最大距离

    private Vector3 disVector;
    private Vector3 position;

    private Quaternion rotation;

    private float eulerX = 0.0F;
    private float eulerY = 20.0F;

    public bool AutoRot { get; set; } = false;

    void Start()
    {
        if (target == null)
        {
            target = new GameObject("camera target");
        }
    }

    void Update()
    {
        RotationAndScale();
    }
    

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0F)
        {
            angle += 360.0F;
        }
        else if (angle > 360.0F)
        {
            angle -= 360.0F;
        }

        return Mathf.Clamp(angle, min, max);
    }

    private void RotationAndScale()
    {
        if (canRote)
        {
            if (Input.GetMouseButton((int)rotBtn))//按下鼠标右键
            {
                eulerX += Input.GetAxis("Mouse X") * xSpeed * 0.05F;
                eulerY -= Input.GetAxis("Mouse Y") * ySpeed * 0.05F;
                eulerY = ClampAngle(eulerY, yMinLimit, yMaxLimit);
            }
            else if(AutoRot)
            {
                eulerX += xSpeed * 0.05F * Time.deltaTime;
                eulerY = ClampAngle(eulerY, yMinLimit, yMaxLimit);
            }
            rotation = Quaternion.Euler(eulerY, eulerX, 0.0F);
        }

        if (canScale)
        {
            if (!UITool.GetUiFromMousePosition(typeof(ScrollRect)))
            {
                distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }

        if (canDrag)
        {
            if (Input.GetMouseButton((int)dragBtn))
            {
                Vector3 moveTarget= transform.GetComponent<Camera>().WorldToScreenPoint(target.transform.position);
                moveTarget.x -= Input.GetAxis("Mouse X") * 10.0F;
                moveTarget.y -= Input.GetAxis("Mouse Y") * 10.0F;
                target.transform.position = transform.GetComponent<Camera>().ScreenToWorldPoint(moveTarget);
            }
        }

        disVector = new Vector3(0.0F, 0.0F, -distance);
        position = rotation * disVector + target.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, damping);
        transform.position = Vector3.Lerp(transform.position, position, damping);
    }
}