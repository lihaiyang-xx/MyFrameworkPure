using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = System.Object;

namespace MyFrameworkPure
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text messageText;
        [SerializeField]
        private Button[] templateBtns;
        [SerializeField]
        private Transform btnParent;

        private Action callback;

        private static MessageBox instance;

        void Awake()
        {
            transform.localPosition = Vector3.zero;
        }

        public static void Show(string message)
        {
            Show("提示", message);
        }

        public static void Show(string message, Action callback)
        {
            Show("提示", message, callback);
        }

        public static void Show(string title, string message, Action callback = null)
        {
            Show(title, message, new[] { "确定" }, new[] { callback });
        }

        public static void Show(string title, string message, string[] btns, params Action[] dosomethings)
        {
            Debug.Assert(btns.Length == dosomethings.Length, "按钮数量和委托数量不一致!");

            if (instance == null)
            {
                instance = GameObjectTool.FindObjectOfType<MessageBox>();
            }
            instance.transform.SetAsLastSibling();
            instance.gameObject.SetActive(true);
            instance.titleText.text = title;
            instance.messageText.text = message;

            instance.btnParent.ClearChild(exclude:x=>!x.gameObject.activeSelf);
            for (var i = 0; i < btns.Length; i++)
            {
                var str = btns[i];
                Button btn = Instantiate(instance.templateBtns[i], instance.btnParent);
                btn.gameObject.SetActive(true);
                btn.GetComponentInChildren<Text>().text = str;
                var locali = i;
                btn.onClick.AddListener(() =>
                {
                    instance.gameObject.SetActive(false);//和下行代码顺序不能换,否则如果委托中再次调用messagebox无效
                    dosomethings[locali]?.Invoke();
                });
            }
        }
    }
}

