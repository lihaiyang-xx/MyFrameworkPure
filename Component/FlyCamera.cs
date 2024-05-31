using UnityEngine;
using System.Collections;
using MyFrameworkPure;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlyCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float damping = 0.05F;//插值速度

    [Header("旋转设置")]
    [SerializeField] private bool canRote = true;//可以旋转
    [SerializeField] private PointerEventData.InputButton rotBtn;
    [SerializeField] private float hSpeed = 4;//水平旋转速度
    [SerializeField] private float vSpeed = 2.5f;//摄像机竖直旋转速度
    [SerializeField] private float vMinLimit = 10.0F;//竖直方向旋转限制最小值
    [SerializeField] private float vMaxLimit = 45.0F;//竖直方向旋转限制最大值

    [Header("拖拽设置")]
    [SerializeField] private bool canDrag = true;

    [SerializeField] private float dragSpeed = 10;
    [SerializeField] private PointerEventData.InputButton dragBtn;

    [Header("缩放设置")]
    [SerializeField] private bool canScale = true;
    [SerializeField] private float mSpeed = 50.0F;//滚轴拉近速度
    [SerializeField] public float distance;//当前摄像机距离
    [SerializeField] private float minDistance = 0.0F;//摄像机最小距离
    [SerializeField] private float maxDistance = 230.0F;//摄像机最大距离

    private Vector3 position;
    private Quaternion rotation;



    public bool AutoRot { get; set; } = false;
    public float MinDistance
    {
        get => minDistance;
        set => minDistance = value;
    }

    public float MaxDistance
    {
        get => maxDistance;
        set => maxDistance = value;
    }

    public float MSpeed
    {
        get => mSpeed;
        set => mSpeed = value;
    }

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private Vector3 OriginLookTargetPos { get; set; }
    private Transform OriginPose { get; set; }


    void Start()
    {
        InputManager inputManager = InputManager.Instance;
        inputManager.onZoom.AddListener(v =>
        {
            if(!target || !canScale)
                return;
            distance -= v * mSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        });

        inputManager.onDrag.AddListener(v =>
        {
            if (!target || !canDrag)
                return;
            Vector3 moveTarget = transform.GetComponent<Camera>().WorldToScreenPoint(target.transform.position);
            moveTarget.x -= v.x * dragSpeed;
            moveTarget.y -= v.y * dragSpeed;
            target.transform.position = transform.GetComponent<Camera>().ScreenToWorldPoint(moveTarget);
        });

        inputManager.onRot.AddListener(v =>
        {
            if (!target || !canRote)
                return;
            ResetTargetPos();
            Vector3 euler = rotation.eulerAngles;
            float eulerY = euler.y + v.x * hSpeed;
            float eulerX = euler.x - v.y * vSpeed;
            eulerX = ClampAngle(eulerX, vMinLimit, vMaxLimit);
            rotation = Quaternion.Euler(eulerX, eulerY, 0.0F);
        });
    }

    void Update()
    {
        if(!target)
            return;
        Vector3 disVector = new Vector3(0, 0, -distance);
        position = rotation * disVector + target.transform.position;

        transform.position = Vector3.Lerp(transform.position, position, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, damping * Time.deltaTime);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -180)
        {
            angle += 360.0F;
        }
        else if (angle > 180)
        {
            angle -= 360.0F;
        }

        return Mathf.Clamp(angle, min, max);
    }


    public void Focus(Transform focusTarget)
    {
        target.position = focusTarget.position;
        rotation = Quaternion.LookRotation(-focusTarget.forward);
        distance = minDistance;
    }

    public void Init(Transform lookTarget,Transform originPose)
    {
        OriginLookTargetPos = lookTarget.position;//记录相机初始目标位置
        OriginPose = originPose;

        target = lookTarget;
        transform.position = originPose.position;
        position = transform.position;

        transform.rotation = Quaternion.LookRotation(target.position-transform.position);
        rotation = transform.rotation;

        distance = Vector3.Distance(target.position, transform.position);
    }

    public void ReInit()
    {
        target.position = OriginLookTargetPos;
        position = OriginPose.position;
        rotation = OriginPose.rotation;
        distance = Vector3.Distance(target.position, position);
    }

    void ResetTargetPos()
    {
        if(StartConfig.Instance.isTest)
            return;
        target.position = OriginLookTargetPos;
    }
}