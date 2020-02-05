using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    [SerializeField] private Text titleText;

    [SerializeField] private InputField inputField;

    [SerializeField] private Button ensureBtn;

    public UnityAction<string> onComplete;
    // Start is called before the first frame update
    void Start()
    {
        ensureBtn.onClick.AddListener(()=>
        {
            onComplete?.Invoke(inputField.text);
            gameObject.SetActive(false);
        });
    }

    public void SetData(string title, string placeholder)
    {
        titleText.text = title;
        inputField.placeholder.GetComponent<Text>().text = placeholder;
    }
}
