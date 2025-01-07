using UnityEngine;

public class FlexViewCamera : MonoBehaviour
{
    [Header("平移设置")]
    public bool enablePan = true; // 是否启用平移功能
    public float panSpeed = 10f; // 平移速度
    public float smoothTime = 0.2f; // 平滑时间

    [Header("旋转设置")]
    public bool enableRotation = true; // 是否启用旋转功能
    public float rotationSpeed = 100f; // 旋转速度
    public float rotationSmoothTime = 0.2f; // 旋转平滑时间
    public float minRotX = -90;
    public float maxRotX = 90;

    [Header("滚轮推进设置")]
    public bool enableZoom = true; // 是否启用滚轮推进功能
    public float zoomSpeed = 10f; // 滚轮推进速度
    public float minHeight = 5f; // 最低高度限制
    public float maxHeight = 50f; // 最高高度限制

    private Vector3 targetPosition; // 平移目标位置
    private Vector3 panVelocity; // 平移缓动速度
    private Vector3 targetEuler; // 目标旋转
    private Vector3 rotationVelocity; // 旋转缓动速度

    private void Start()
    {
        targetPosition = transform.position; // 初始化目标位置
        targetEuler = transform.eulerAngles; // 初始化目标旋转
    }

    private void Update()
    {
        HandleInput();
        SmoothMove();
        SmoothRotate();
    }

    private void HandleInput()
    {
        // 平移
        if (enablePan && Input.GetMouseButton(0)) // 鼠标左键平移
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveZ = Input.GetAxis("Mouse Y");

            Vector3 move = new Vector3(-moveX, 0, -moveZ);
            move = transform.right * move.x + Vector3.ProjectOnPlane(transform.forward,Vector3.up).normalized * move.z; // 基于相机的 right 和 forward 方向
            targetPosition += move * panSpeed * Time.deltaTime;
        }

        // 旋转（缓动）
        if (enableRotation && Input.GetMouseButton(1)) // 鼠标右键旋转
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // 叠加旋转，计算目标旋转
            targetEuler.y += rotationX;
            targetEuler.x -= rotationY;

            targetEuler.x = Mathf.Clamp(targetEuler.x, minRotX, maxRotX);
        }

        // 滚轮推进（沿正方向移动，并限制高度）
        if (enableZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f) // 检测滚轮输入
            {
                // 计算相机的移动方向（沿正方向推进）
                Vector3 direction = transform.forward;

                // 计算目标位置
                Vector3 newTargetPosition = targetPosition + direction * scroll * zoomSpeed;

                // 检查高度限制
                if (newTargetPosition.y >= minHeight && newTargetPosition.y <= maxHeight)
                {
                    targetPosition = newTargetPosition; // 只有在高度范围内时才更新目标位置
                }
            }
        }
    }

    private void SmoothMove()
    {
        // 平移缓动
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref panVelocity, smoothTime);
    }

    private void SmoothRotate()
    {
        // 旋转缓动
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetEuler), rotationSmoothTime * Time.deltaTime);
    }
}