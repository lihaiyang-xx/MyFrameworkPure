using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    /// <summary>
    /// 显示帧率(OnGUI方式)
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private int fontSize = 30;
        [SerializeField] private Color color = Color.black;

        float deltaTime = 0.0f;

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, fontSize);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = fontSize;
            style.normal.textColor = color;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}
