using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 范围内随机实例化预设
/// </summary>
public class RandomGeneratorWindow : EditorWindow
{
    private int buildingNum;
    private float halfSideLength = 5000;
    private float minScale = 0.8f;
    private float maxScale = 1.2f;
    private Transform buildingRoot;
    private Transform roadRoot;

    [SerializeField]
    private List<GameObject> prefabList;

    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;

    private Vector2 scrollPos;

    [MenuItem("Tools/创建建筑")]
    static void Init()
    {
        RandomGeneratorWindow window = EditorWindow.GetWindow<RandomGeneratorWindow>();
        window.Show();
    }

    void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        serializedProperty = serializedObject.FindProperty("prefabList");
    }

    // Update is called once per frame
    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        
        buildingNum = EditorGUILayout.IntField("建筑数量:",buildingNum);
        halfSideLength = EditorGUILayout.FloatField("地形边长(一半):", halfSideLength);
        minScale = EditorGUILayout.FloatField("随机缩放(最小):", minScale);
        maxScale = EditorGUILayout.FloatField("随机缩放(最大):", maxScale);
        buildingRoot = EditorGUILayout.ObjectField("建筑组:",buildingRoot, typeof(Transform), true) as Transform;

        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedProperty, new GUIContent("建筑预设:"), true);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("生成"))
        {
            buildingRoot.ClearChild(true);
            int i = 0;
            while (i++ < buildingNum)
            {
                float randomX = Random.Range(-halfSideLength, halfSideLength);
                float randomZ = Random.Range(-halfSideLength, halfSideLength);
                GameObject prefab = prefabList.Random();
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab, buildingRoot) as GameObject;
                instance.transform.position = new Vector3(randomX, 0, randomZ);
                instance.transform.eulerAngles = new Vector3(0, Random.value * 360, 0);
                instance.transform.localScale *= Random.Range(minScale, maxScale);
            }
        }

        roadRoot = EditorGUILayout.ObjectField("道路组:",roadRoot,typeof(Transform),true) as Transform;
        if (GUILayout.Button("删除道路上建筑"))
        {
            IEnumerable<Bounds> boundses = roadRoot.GetComponentsInChildren<BoxCollider>().Select(x => x.bounds);
            for (int i = buildingRoot.childCount - 1; i >= 0; i--)
            {
                Transform building = buildingRoot.GetChild(i);
                if (boundses.Count(x =>Vector3.Distance(x.ClosestPoint(building.position),building.position) < 100) > 0)
                {
                    DestroyImmediate(building.gameObject);
                }
            }
        }
        EditorGUILayout.EndScrollView();
    }
}
