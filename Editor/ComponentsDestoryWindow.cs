using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MyFrameworkPure;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 批量删除场景中的组件窗口
    /// </summary>
    public class ComponentsDestoryWindow : EditorWindow
    {
        [SerializeField]
        private List<Component> componentList;

        [SerializeField]
        private bool includeChildren;

        private SerializedObject serializedObject;
        private SerializedProperty serializedProperty;

        [MenuItem("Tools/EditorTools/批量删除场景组件窗口")]
        static void InitWindow()
        {
            ComponentsDestoryWindow window = EditorWindow.GetWindow<ComponentsDestoryWindow>();
            window.Show();
        }

        void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            serializedProperty = serializedObject.FindProperty("componentList");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedProperty, true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            includeChildren = EditorGUILayout.Toggle("包括子物体", includeChildren);

            if (GUILayout.Button("删除"))
            {
                if (Selection.activeGameObject == null)
                {
                    Debug.LogError("所选物体为空!");
                    return;
                }
                foreach (Component component in componentList)
                {
                    if (includeChildren)
                    {
                        Selection.activeGameObject.GetComponentsInChildren(component.GetType()).DestroyImmediate();

                    }
                    else
                        DestroyImmediate(Selection.activeGameObject.GetComponent(component.GetType()));
                }
            }
        }
    }
}

