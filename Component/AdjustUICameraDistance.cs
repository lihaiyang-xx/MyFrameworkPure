using UnityEngine;

/// <summary>
/// ����ӦUI�������
/// </summary>
[RequireComponent(typeof(Canvas))]
public class AdjustUICameraDistance : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [Tooltip("������UI��������")]
    [SerializeField] private Vector3 desiredScale = new Vector3(0.01f, 0.01f, 0.01f);

    private Canvas canvas;
    private RectTransform canvasRect;
    // �趨��Canvas����

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.transform.localScale = desiredScale;

        canvasRect = GetComponent<RectTransform>();

        if (uiCamera == null)
        {
            uiCamera = Camera.main;
        }

        AdjustCameraDistance();
    }

#if UNITY_EDITOR
    void Update()
    {
        AdjustCameraDistance();
    }
#endif

    /// <summary>
    /// ����ӦUI�������
    /// </summary>
    public void AdjustCameraDistance()
    {
        if (uiCamera == null) return;
        //Debug.Log(Screen.width + " " + Screen.height);
        int minValue = Mathf.Min(Screen.width, Screen.height);
        int maxValue = Mathf.Max(Screen.width, Screen.height);

        canvasRect.sizeDelta = new Vector2(maxValue,minValue);

        // ��ȡ��Ļ�Ŀ�͸�
        float screenHeight = Screen.height * desiredScale.y;

        // ����FOV����Ļ�߶ȼ���UI�����Ӧ����Canvas�ľ���
        float distance = screenHeight * 0.5f / Mathf.Tan(uiCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // �޸��������λ��
        uiCamera.transform.position = transform.position - uiCamera.transform.forward * distance;
    }
}
