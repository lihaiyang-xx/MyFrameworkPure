using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 流程图窗口
    /// </summary>
    public class FlowchartDesignerWindow : EditorWindow
    {
        private Vector2 mActionScrollPosition;
        private Vector2 mActionViewSize = new Vector2(300, 800);
        private Vector2 mActionScrollSize = new Vector2(300, 1600);
        private Vector2 mChartScrollPosition;
        private Vector2 mChartAreaOffset;

        private Rect mChartViewRect = new Rect(300, 20, 0, 0);
        private Rect mChartScrollRect = new Rect(0, 0, 30000, 30000);

        private float mSearchFieldWidth = 300;
        private float mWindowWidth;//窗体宽度
        private float mWindowHeight;//窗体高度

        private int mLogicZoom = 1;//逻辑面板缩放
        private const int ScrollbarWidth = 20;//滚动条宽度

        private string mSearchString;

        private Material mGridMaterial;

        [MenuItem("Tools/流程图 编辑器 %F1")]
        static void OpenLogicDesignerWindow()
        {
            FlowchartDesignerWindow window = EditorWindow.GetWindow<FlowchartDesignerWindow>();
            window.titleContent = new GUIContent("流程图 编辑器");
            window.Show();
        }

        void OnEnable()
        {
            mGridMaterial = new Material(Shader.Find("Hidden/Behavior Designer/Grid"))
            {
                hideFlags = HideFlags.HideAndDontSave,
                shader = { hideFlags = HideFlags.HideAndDontSave }
            };
        }


        private void OnGUI()
        {
            mWindowWidth = position.width;
            mWindowHeight = position.height;

            //行为面板
            GUILayout.BeginVertical();
            mSearchString = GUILayout.TextField(mSearchString, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.Width(mSearchFieldWidth));
            GUILayout.Space(4f);
            mActionScrollPosition = GUILayout.BeginScrollView(mActionScrollPosition, false, true, GUILayout.Width(mActionViewSize.x));
            GUILayout.Button("xxx");
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            //流程图面板
            mChartViewRect.width = mWindowWidth - mActionViewSize.x;
            mChartViewRect.height = mWindowHeight - mChartViewRect.y;
            mChartScrollPosition = GUI.BeginScrollView(mChartViewRect, mChartScrollPosition, mChartScrollRect, true, true);

            GUI.EndScrollView();
            mChartAreaOffset = mChartScrollPosition - mChartViewRect.size * 0.5f;
            DrawGrid();
        }

        //绘制流程图网格
        private void DrawGrid()
        {
            //绘制黑色网格线
            mGridMaterial.SetPass((!EditorGUIUtility.isProSkin) ? 1 : 0);
            GL.PushMatrix();
            GL.Begin(1);
            DrawGridLines(10f * this.mLogicZoom, new Vector2(this.mChartAreaOffset.x % 10f * this.mLogicZoom, this.mChartAreaOffset.y % 10f * this.mLogicZoom));
            GL.End();
            GL.PopMatrix();

            //绘制白色网格线
            mGridMaterial.SetPass((!EditorGUIUtility.isProSkin) ? 3 : 2);
            GL.PushMatrix();
            GL.Begin(1);
            DrawGridLines(50f * this.mLogicZoom, new Vector2(this.mChartAreaOffset.x % 50f * this.mLogicZoom, this.mChartAreaOffset.y % 50f * this.mLogicZoom));
            GL.End();
            GL.PopMatrix();
        }

        //绘制线条
        private void DrawGridLines(float gridSize, Vector2 offset)
        {
            float num = mChartViewRect.x + offset.x;
            if (offset.x < 0f)
            {
                num += gridSize;
            }
            for (float num2 = num; num2 < mChartViewRect.x + mChartViewRect.width - ScrollbarWidth; num2 += gridSize)
            {
                this.DrawLine(new Vector2(num2, mChartViewRect.y), new Vector2(num2, mChartViewRect.y + mChartViewRect.height - ScrollbarWidth));
            }
            float num3 = mChartViewRect.y + offset.y;
            if (offset.y < 0f)
            {
                num3 += gridSize;
            }
            for (float num4 = num3; num4 < mChartViewRect.y + mChartViewRect.height - ScrollbarWidth; num4 += gridSize)
            {
                this.DrawLine(new Vector2(mChartViewRect.x, num4), new Vector2(mChartViewRect.x + mChartViewRect.width - ScrollbarWidth, num4));
            }
        }

        private void DrawLine(Vector2 p1, Vector2 p2)
        {
            GL.Vertex(p1);
            GL.Vertex(p2);
        }
    }
}

