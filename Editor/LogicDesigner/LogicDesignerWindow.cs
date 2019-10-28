using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogicDesignerWindow : EditorWindow
{
    private Vector2 mActionScrollPosition;
    private Vector2 mActionSize = new Vector2(300,800);
    private Vector2 mActionScrollSize = new Vector2(300, 1600);
    private Vector2 mLogicScrollPosition;
    private Vector2 mLogicAreaOffset;

    private Rect mLogicRect = new Rect(300,20,0,0);
    private Rect mLogicScrollRect = new Rect(0,0,30000,30000);

    private float mSearchFieldWidth = 300;
    private float mWindowWidth;//窗体宽度
    private float mWindowHeight;//窗体高度

    private int mLogicZoom = 1;//逻辑面板缩放

    private string mSearchString;

    private Material mGridMaterial;

    [MenuItem("Tools/Logic Designer Editor _F1")]
    static void OpenLogicDesignerWindow()
    {
        LogicDesignerWindow window = EditorWindow.GetWindow<LogicDesignerWindow>();
        window.titleContent = new GUIContent("Logic Designer Window");
        window.Show();
    }

    void OnEnable()
    {
        mGridMaterial = new Material(Shader.Find("Hidden/Behavior Designer/Grid"))
        {
            hideFlags = HideFlags.HideAndDontSave, shader = {hideFlags = HideFlags.HideAndDontSave}
        };
    }


    private void OnGUI()
    {
        mWindowWidth = position.width;
        mWindowHeight = position.height;

        GUILayout.BeginVertical();
        mSearchString = GUILayout.TextField(mSearchString,GUI.skin.FindStyle("ToolbarSeachTextField"),GUILayout.Width(mSearchFieldWidth));
        GUILayout.Space(4f);
        mActionScrollPosition = GUILayout.BeginScrollView(mActionScrollPosition,false,true,GUILayout.Width(mActionSize.x));
        GUILayout.Button("xxx");
        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        mLogicRect.width = mWindowWidth-mActionSize.x;
        mLogicRect.height = mWindowHeight-mLogicRect.y;
        Debug.Log(mLogicRect);
        mLogicScrollPosition = GUI.BeginScrollView(mLogicRect, mLogicScrollPosition,mLogicScrollRect,true,true);

        GUI.EndScrollView();
        mLogicAreaOffset = mLogicScrollPosition - mLogicRect.size * 0.5f;
        DrawGrid();
    }

    //绘制逻辑区网格
    private void DrawGrid()
    {
        this.mGridMaterial.SetPass((!EditorGUIUtility.isProSkin) ? 1 : 0);
        GL.PushMatrix();
        GL.Begin(1);
        this.DrawGridLines(10f * this.mLogicZoom, new Vector2(this.mLogicAreaOffset.x % 10f * this.mLogicZoom, this.mLogicAreaOffset.y % 10f * this.mLogicZoom));
        //this.DrawGridLines(10f * this.mLogicZoom, mLogicAreaOffset);
        GL.End();
        GL.PopMatrix();
        this.mGridMaterial.SetPass((!EditorGUIUtility.isProSkin) ? 3 : 2);
        GL.PushMatrix();
        GL.Begin(1);
        this.DrawGridLines(50f * this.mLogicZoom, new Vector2(this.mLogicAreaOffset.x % 50f * this.mLogicZoom, this.mLogicAreaOffset.y % 50f * this.mLogicZoom));
        //this.DrawGridLines(50f * this.mLogicZoom, mLogicAreaOffset);
        GL.End();
        GL.PopMatrix();
    }

    //绘制线条
    private void DrawGridLines(float gridSize, Vector2 offset)
    {
        float num = mLogicRect.x + offset.x;
        if (offset.x < 0f)
        {
            num += gridSize;
        }
        for (float num2 = num; num2 < mLogicRect.x + mLogicRect.width; num2 += gridSize)
        {
            this.DrawLine(new Vector2(num2, mLogicRect.y), new Vector2(num2, mLogicRect.y + mLogicRect.height));
        }
        float num3 = mLogicRect.y + offset.y;
        if (offset.y < 0f)
        {
            num3 += gridSize;
        }
        for (float num4 = num3; num4 < mLogicRect.y + mLogicRect.height; num4 += gridSize)
        {
            this.DrawLine(new Vector2(mLogicRect.x, num4), new Vector2(mLogicRect.x + mLogicRect.width, num4));
        }
    }

    private void DrawLine(Vector2 p1, Vector2 p2)
    {
        GL.Vertex(p1);
        GL.Vertex(p2);
    }
}
