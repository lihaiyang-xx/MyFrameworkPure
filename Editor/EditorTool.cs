using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class EditorTool : MonoBehaviour
    {
        [MenuItem("Tools/重命名Button的Text")]
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

        [MenuItem("Tools/Text内容作为物体名称")]
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

        [MenuItem("Tools/text替换成textmeshpro")]
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

        [MenuItem("Tools/textmeshpro替换成text")]
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

        [MenuItem("Tools/放大选中Text字体")]
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
    }
}

