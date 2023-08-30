using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class SelectCard : MonoBehaviour
    {
        [SerializeField] private Transform toggles;
        [SerializeField] private Transform panels;
        [SerializeField] private Color onTextColor;
        [SerializeField] private Color offTextColor;

        public UnityAction<int> onValueChanged;
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < toggles.childCount; i++)
            {
                if (i >= panels.childCount)
                    break;
                var index = i;
                Toggle toggle = toggles.GetChild(i).GetComponent<Toggle>();
                toggle.onValueChanged.AddListener(v =>
                {
                    toggle.GetComponentInChildren<Text>().color = v ? onTextColor : offTextColor;
                    panels.GetChild(index).gameObject.SetActive(v);
                    if (v)
                        onValueChanged?.Invoke(index);
                });
            }

            Toggle firstToggle = toggles.GetChild(0).GetComponent<Toggle>();
            firstToggle.isOn = true;
            firstToggle.onValueChanged?.Invoke(true);
        }

        public Transform ToggleParent => toggles;
        public Transform PanelParent => panels;
    }
}

