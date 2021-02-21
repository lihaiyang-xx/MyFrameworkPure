using System;
using MyFrameworkPure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    /// <summary>
    /// 输入框面板,包含标题,输入提示文字,确认,取消按钮
    /// </summary>
    public class InputBox : MonoBehaviour
    {
        [SerializeField] private Text titleText;

        [SerializeField] private InputField inputField;

        [SerializeField] private Button ensureBtn;
        [SerializeField] private Button cancelBtn;

        public UnityAction<string> onComplete;
        public Predicate<string> onVaild;

        private static InputBox instance;
        // Start is called before the first frame update
        void Start()
        {
            ensureBtn.onClick.AddListener(() =>
            {
                if (onVaild == null || onVaild(inputField.text))
                {
                    onComplete?.Invoke(inputField.text);
                    gameObject.SetActive(false);
                }
            });

            cancelBtn.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public static void Show(string title, string placeholder, UnityAction<string> onComplete, Predicate<string> onvaild = null)
        {
            if (instance == null)
            {
                instance = GameObjectTool.FindObjectOfType<InputBox>();
            }
            instance.transform.SetAsLastSibling();
            instance.gameObject.SetActive(true);
            instance.titleText.text = title;
            instance.inputField.placeholder.GetComponent<Text>().text = placeholder;
            instance.onComplete = onComplete;
            instance.onVaild = onvaild;
        }
    }
}

