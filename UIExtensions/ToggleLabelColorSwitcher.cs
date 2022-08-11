using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleLabelColorSwitcher : MonoBehaviour
    {
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;

        // Start is called before the first frame update
        void Start()
        {
            Text label = GetComponentInChildren<Text>();
            Toggle toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(v =>
            {
                label.color = v ? onColor : offColor;
            });
            toggle.onValueChanged.Invoke(toggle.isOn);
        }
    }
}

