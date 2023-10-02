using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Graphic graphic;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;

    void OnEnable()
    {
        graphic.color = normalColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventTriggerListener.Get(gameObject).onEnter += d =>
        {
            graphic.color = hoverColor;
        };
        EventTriggerListener.Get(gameObject).onExit += d =>
        {
            graphic.color = normalColor;
        };
    }
}
