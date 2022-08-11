using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    /// <summary>
    /// 标签文本
    /// </summary>
    public class ShortLabel : MonoBehaviour
    {
        private static ShortLabel instance;

        private Text label;


        void Awake()
        {
            label = GetComponentInChildren<Text>();
        }

        void Update()
        {
            instance.transform.position = Input.mousePosition;
        }

        public void SetText(string str)
        {
            label.text = str;
        }

        public static void Show(string str)
        {
            if (instance == null)
                instance = GameObjectTool.FindObjectOfType<ShortLabel>();
            instance.gameObject.SetActive(true);
            instance.SetText(str);
        }

        public static void Hide()
        {
            if (instance == null)
                instance = GameObjectTool.FindObjectOfType<ShortLabel>();
            instance.gameObject.SetActive(false);
        }
    }
}

