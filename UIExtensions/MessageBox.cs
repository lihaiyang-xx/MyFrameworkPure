using System;
using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class MessageBox:MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text messageText;
    [SerializeField]
    private Button ensureBtn;

    private Action callback;

    private static MessageBox instance;

    void Awake()
    {
        transform.localPosition = Vector3.zero;
        ensureBtn.onClick.AddListener(() =>
        {
            instance.gameObject.SetActive(false);
            callback?.Invoke();
        });
    }

    public static void Show(string message)
    {
        Show("提示",message);
    }


    public static void Show(string title,string message, Action callback=null)
    {
        if (instance == null)
        {
            instance = GameObjectTool.FindObjectOfType<MessageBox>();
        }
        instance.transform.SetAsLastSibling();
        instance.gameObject.SetActive(true);
        instance.titleText.text = title;
        instance.messageText.text = message;
        instance.callback = callback;
    }
}
