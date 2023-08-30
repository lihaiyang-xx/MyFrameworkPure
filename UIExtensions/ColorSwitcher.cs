using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;
 

    // Start is called before the first frame update
    void Start()
    {
        EventTriggerListener.Get(gameObject).onEnter += d =>
        {
            image.color = hoverColor;
        };

        EventTriggerListener.Get(gameObject).onExit += d =>
        {
            image.color = normalColor;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
