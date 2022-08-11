using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestFitText : MonoBehaviour
{
    [SerializeField] private int minFontSize = 12;
    [SerializeField] private int maxFontSize = 20;

    private RectTransform rectTransform;
    private Text label;

    private string lastText;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        label = GetComponent<Text>();
        lastText = label.text;
        label.resizeTextForBestFit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (label.text == lastText)
        {
            return;
        }

        lastText = label.text;
        CalculateFontSize();
    }

    void CalculateFontSize()
    {
        while (label.preferredHeight > rectTransform.rect.height)
        {
            label.fontSize--;
            if (label.fontSize <= minFontSize)
            {
                label.fontSize = minFontSize;
                break;
            }
                
        }

        while (label.preferredHeight < rectTransform.rect.height)
        {
            label.fontSize++;
            if (label.fontSize >= maxFontSize)
            {
                label.fontSize = maxFontSize;
                break;
            }
        }
    }
}
