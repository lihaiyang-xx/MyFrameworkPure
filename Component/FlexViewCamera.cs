using UnityEngine;

public class FlexViewCamera : MonoBehaviour
{
    [Header("ƽ������")]
    public bool enablePan = true; // �Ƿ�����ƽ�ƹ���
    public float panSpeed = 10f; // ƽ���ٶ�
    public float smoothTime = 0.2f; // ƽ��ʱ��

    [Header("��ת����")]
    public bool enableRotation = true; // �Ƿ�������ת����
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public float rotationSmoothTime = 0.2f; // ��תƽ��ʱ��
    public float minRotX = -90;
    public float maxRotX = 90;

    [Header("�����ƽ�����")]
    public bool enableZoom = true; // �Ƿ����ù����ƽ�����
    public float zoomSpeed = 10f; // �����ƽ��ٶ�
    public float minHeight = 5f; // ��͸߶�����
    public float maxHeight = 50f; // ��߸߶�����

    private Vector3 targetPosition; // ƽ��Ŀ��λ��
    private Vector3 panVelocity; // ƽ�ƻ����ٶ�
    private Vector3 targetEuler; // Ŀ����ת
    private Vector3 rotationVelocity; // ��ת�����ٶ�

    private void Start()
    {
        targetPosition = transform.position; // ��ʼ��Ŀ��λ��
        targetEuler = transform.eulerAngles; // ��ʼ��Ŀ����ת
    }

    private void Update()
    {
        HandleInput();
        SmoothMove();
        SmoothRotate();
    }

    private void HandleInput()
    {
        // ƽ��
        if (enablePan && Input.GetMouseButton(0)) // ������ƽ��
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveZ = Input.GetAxis("Mouse Y");

            Vector3 move = new Vector3(-moveX, 0, -moveZ);
            move = transform.right * move.x + Vector3.ProjectOnPlane(transform.forward,Vector3.up).normalized * move.z; // ��������� right �� forward ����
            targetPosition += move * panSpeed * Time.deltaTime;
        }

        // ��ת��������
        if (enableRotation && Input.GetMouseButton(1)) // ����Ҽ���ת
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // ������ת������Ŀ����ת
            targetEuler.y += rotationX;
            targetEuler.x -= rotationY;

            targetEuler.x = Mathf.Clamp(targetEuler.x, minRotX, maxRotX);
        }

        // �����ƽ������������ƶ��������Ƹ߶ȣ�
        if (enableZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f) // ����������
            {
                // ����������ƶ��������������ƽ���
                Vector3 direction = transform.forward;

                // ����Ŀ��λ��
                Vector3 newTargetPosition = targetPosition + direction * scroll * zoomSpeed;

                // ���߶�����
                if (newTargetPosition.y >= minHeight && newTargetPosition.y <= maxHeight)
                {
                    targetPosition = newTargetPosition; // ֻ���ڸ߶ȷ�Χ��ʱ�Ÿ���Ŀ��λ��
                }
            }
        }
    }

    private void SmoothMove()
    {
        // ƽ�ƻ���
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref panVelocity, smoothTime);
    }

    private void SmoothRotate()
    {
        // ��ת����
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetEuler), rotationSmoothTime * Time.deltaTime);
    }
}