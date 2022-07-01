using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextOverflowEllipsis : MonoBehaviour
{
    private Text label;

    private string originValue;

    private bool isEllipsis = false;

    public void SetTextWithEllipsis(string value)
    {
        if (label == null)
            label = GetComponent<Text>();
        originValue = value;
        var generator = new TextGenerator();
        var rectTransform = label.GetComponent<RectTransform>();
        var settings = label.GetGenerationSettings(rectTransform.rect.size);
        generator.Populate(value, settings);

        // trncate visible value and add ellipsis
        var characterCountVisible = generator.characterCountVisible;
        var updatedText = value;
        if (value.Length > characterCountVisible)
        {
            updatedText = value.Substring(0, characterCountVisible - 1);
            updatedText += "…";
            isEllipsis = true;
        }
        else
        {
            isEllipsis = false;
        }

        label.text = updatedText;
    }
}
