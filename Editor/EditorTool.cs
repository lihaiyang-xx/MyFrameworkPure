using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MyFrameworkPure;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class EditorTool : MonoBehaviour
    {
        [MenuItem("Tools/EditorTools/重命名子物体的Text内容")]
        static void RenameButtonText()
        {
            GameObject[] gos = Selection.gameObjects;
            foreach (GameObject go in gos)
            {
                Text labelText = go.GetComponentInChildren<Text>();
                if (labelText)
                    labelText.text = go.name;
                TMP_Text textPro = go.GetComponentInChildren<TMP_Text>();
                if (textPro)
                    textPro.text = go.name;
            }
        }

        [MenuItem("Tools/EditorTools/Text内容作为物体名称")]
        static void RenameText()
        {
            GameObject[] gos = Selection.gameObjects;
            foreach (GameObject go in gos)
            {
                Text labelText = go.GetComponentInChildren<Text>();
                if (labelText)
                    go.name = labelText.text;
                TMP_Text textPro = go.GetComponentInChildren<TMP_Text>();
                if (textPro)
                    go.name = textPro.text;
            }
        }

        [MenuItem("Tools/EditorTools/text替换成textmeshpro")]
        static void ReplaceToTextMeshPro()
        {
            Text[] texts = Selection.activeGameObject.GetComponentsInChildren<Text>();
            foreach (Text label in texts)
            {
                GameObject go = label.gameObject;
                string text = label.text;
                Color color = label.color;
                int size = label.fontSize;
                DestroyImmediate(label);

                TextMeshProUGUI tmPro = go.AddComponent<TextMeshProUGUI>();
                tmPro.color = color;
                tmPro.fontSize = size;
                tmPro.text = text;
            }
        }

        [MenuItem("Tools/EditorTools/textmeshpro替换成text")]
        static void ReplaceToText()
        {
            TextMeshProUGUI[] texts = Selection.activeGameObject.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI tmPro in texts)
            {
                GameObject go = tmPro.gameObject;
                string text = tmPro.text;
                Color color = tmPro.color;
                float size = tmPro.fontSize;
                DestroyImmediate(tmPro);

                Text label = go.AddComponent<Text>();
                label.color = color;
                label.fontSize = (int)size;
                label.text = text;
            }
        }

        [MenuItem("Tools/EditorTools/放大选中Text字体")]
        static void ScaleUpTextFont()
        {
            Text[] texts = Selection.activeGameObject.GetComponentsInChildren<Text>();

            foreach (var label in texts)
            {
                label.fontSize *= 10;
                label.transform.localScale *= 0.1f;
                label.horizontalOverflow = HorizontalWrapMode.Overflow;
                label.verticalOverflow = VerticalWrapMode.Overflow;
            }
        }

        [MenuItem("Tools/EditorTools/重新编译脚本 %#R")]
        static void ReCompileScripts()
        {
            Debug.Log("重新编译中...");
            string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
            string[] dllFiles = Directory.GetFiles(tempPath, "*.dll");
            foreach (var file in dllFiles)
            {
                File.Delete(file);
            }
#if UNITY_2019_3_OR_NEWER
                 CompilationPipeline.RequestScriptCompilation();
#elif UNITY_2017_1_OR_NEWER
            var editorAssembly = Assembly.GetAssembly(typeof(Editor));
            var editorCompilationInterfaceType = editorAssembly.GetType("UnityEditor.Scripting.ScriptCompilation.EditorCompilationInterface");
            var dirtyAllScriptsMethod = editorCompilationInterfaceType.GetMethod("DirtyAllScripts", BindingFlags.Static | BindingFlags.Public);
            dirtyAllScriptsMethod.Invoke(editorCompilationInterfaceType, null);
#endif
        }
    }
}

