using UnityEngine;

/// <summary>
/// 自适应UI相机距离
/// </summary>
[RequireComponent(typeof(Canvas))]
public class AdjustUICameraDistance : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [Tooltip("期望的UI画布缩放")]
    [SerializeField] private Vector3 desiredScale = new Vector3(0.01f, 0.01f, 0.01f);

    private Canvas canvas;
    private RectTransform canvasRect;
    // 设定的Canvas缩放

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
    /// 自适应UI相机距离
    /// </summary>
    public void AdjustCameraDistance()
    {
        if (uiCamera == null) return;
        //Debug.Log(Screen.width + " " + Screen.height);
        int minValue = Mathf.Min(Screen.width, Screen.height);
        int maxValue = Mathf.Max(Screen.width, Screen.height);

        canvasRect.sizeDelta = new Vector2(maxValue,minValue);

        // 获取屏幕的宽和高
        float screenHeight = Screen.height * desiredScale.y;

        // 根据FOV和屏幕高度计算UI摄像机应该与Canvas的距离
        float distance = screenHeight * 0.5f / Mathf.Tan(uiCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // 修改摄像机的位置
        uiCamera.transform.position = transform.position - uiCamera.transform.forward * distance;
    }
}
