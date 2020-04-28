using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

public class BatchCreatePrefabWindow : EditorWindow
{
    [MenuItem("Tools/批量生成Prefab")]

    static void AddWindow()
    {
        //创建窗口
        BatchCreatePrefabWindow window = (BatchCreatePrefabWindow)EditorWindow.GetWindow(typeof(BatchCreatePrefabWindow), false, "批量生成Prefab");
        window.Show();

    }

    //输入文字的内容
    private string PrefabPath = "Assets/Resources/";
    private string ObjPath = @"Assets/Obj/";
    GameObject[] selectedGameObjects;



    [InitializeOnLoadMethod]
    public void Awake()
    {
        OnSelectionChange();
    }
    void OnGUI()
    {
        GUIStyle text_style = new GUIStyle();
        text_style.fontSize = 15;
        text_style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab导出路径:");
        PrefabPath = EditorGUILayout.TextField(PrefabPath);
        if (GUILayout.Button("浏览"))
        { EditorApplication.delayCall += OpenPrefabFolder; }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("    Obj导出路径:");
        ObjPath = EditorGUILayout.TextField(ObjPath);
        if (GUILayout.Button("浏览"))
        { EditorApplication.delayCall += OpenObjFolder; }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("当前选中了" + selectedGameObjects.Length + "个物体", text_style);
        if (GUILayout.Button("如果包含动态创建的Mesh，请先点击生成Obj", GUILayout.MinHeight(20)))
        {
            foreach (GameObject m in selectedGameObjects)
            {
                CreateObj(m);
            }
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("生成当前选中物体的Prefab", GUILayout.MinHeight(20)))
        {
            if (selectedGameObjects.Length <= 0)
            {
                //打开一个通知栏  
                this.ShowNotification(new GUIContent("未选择所要导出的物体"));
                return;
            }
            if (!Directory.Exists(PrefabPath))
            {
                Directory.CreateDirectory(PrefabPath);
            }
            foreach (GameObject m in selectedGameObjects)
            {
                CreatePrefab(m, m.name);
            }
            AssetDatabase.Refresh();
        }
    }

    void OpenPrefabFolder()
    {
        string path = EditorUtility.OpenFolderPanel("选择要导出的路径", "", "");
        if (!path.Contains(Application.dataPath))
        {
            Debug.LogError("导出路径应在当前工程目录下");
            return;
        }
        if (path.Length != 0)
        {
            int firstindex = path.IndexOf("Assets");
            PrefabPath = path.Substring(firstindex) + "/";
            EditorUtility.FocusProjectWindow();
        }
    }

    void OpenObjFolder()
    {
        string path = EditorUtility.OpenFolderPanel("选择要导出的路径", "", "");
        if (!path.Contains(Application.dataPath))
        {
            Debug.LogError("导出路径应在当前工程目录下");
            return;
        }
        if (path.Length != 0)
        {
            int firstindex = path.IndexOf("Assets");
            ObjPath = path.Substring(firstindex) + "/";
            EditorUtility.FocusProjectWindow();
        }
    }

    void CreateObj(GameObject go)
    {
        if (!Directory.Exists(ObjPath))
        {
            Directory.CreateDirectory(ObjPath);
        }
        MeshFilter[] meshfilters = go.GetComponentsInChildren<MeshFilter>();
        if (meshfilters.Length > 0)
        {
            for (int i = 0; i < meshfilters.Length; i++)
            {
                ObjExporter.MeshToFile(meshfilters[i], ObjPath + meshfilters[i].gameObject.name + ".obj");

            }
        }
    }
    /// <summary>
    /// 此函数用来根据某物体创建指定名字的Prefab
    /// </summary>
    /// <param name="go">选定的某物体</param>
    /// <param name="name">物体名</param>
    /// <returns>void</returns>
    void CreatePrefab(GameObject go, string name)
    {
        //先创建一个空的预制物体
        //预制物体保存在工程中路径，可以修改("Assets/" + name + ".prefab");
        GameObject tempPrefab = PrefabUtility.SaveAsPrefabAsset(go,PrefabPath + name + ".prefab");

        MeshFilter[] meshfilters = go.GetComponentsInChildren<MeshFilter>();
        if (meshfilters.Length > 0)
        {
            MeshFilter[] prefabmeshfilters = tempPrefab.GetComponentsInChildren<MeshFilter>();
            for (int i = 0; i < meshfilters.Length; i++)
            {
                Mesh m_mesh = AssetDatabase.LoadAssetAtPath<Mesh>(ObjPath + meshfilters[i].gameObject.name + ".obj");
                prefabmeshfilters[i].sharedMesh = m_mesh;
            }
        }
        
        PrefabUtility.ConnectGameObjectToPrefab(go, tempPrefab);
        //返回创建后的预制物体
    }

    void OnInspectorUpdate()
    {
        //这里开启窗口的重绘，不然窗口信息不会刷新
        this.Repaint();
    }

    void OnSelectionChange()
    {
        //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
        selectedGameObjects = Selection.gameObjects;

    }
}


public class ObjExporter
{

    public static string MeshToString(MeshFilter mf)
    {
        Mesh m = mf.sharedMesh;
        //  Material[] mats = mf.GetComponent<MeshRenderer>().sharedMaterials;

        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(mf.name).Append("\n");
        for (int i = 0; i < m.vertices.Length; i++)
            sb.Append(string.Format("v {0} {1} {2}\n", -m.vertices[i].x, m.vertices[i].y, m.vertices[i].z));
        sb.Append("\n");
        for (int i = 0; i < m.normals.Length; i++)
            sb.Append(string.Format("vn {0} {1} {2}\n", -m.normals[i].x, m.normals[i].y, m.normals[i].z));
        sb.Append("\n");
        for (int i = 0; i < m.uv.Length; i++)
            sb.Append(string.Format("vt {0} {1}\n", m.uv[i].x, m.uv[i].y));

        for (int material = 0; material < m.subMeshCount; material++)
        {
            sb.Append("\n");

            int[] triangles = m.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                //Because we inverted the x-component, we also needed to alter the triangle winding.
                sb.Append(string.Format("f {1}/{1}/{1} {0}/{0}/{0} {2}/{2}/{2}\n",
                    triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
            }
        }
        return sb.ToString();
    }

    public static void MeshToFile(MeshFilter mf, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write(MeshToString(mf));
        }
    }
}