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

        public UnityAction<int> onValueChanged;
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < toggles.childCount; i++)
            {
                if (i >= panels.childCount)
                    break;
                var index = i;
                toggles.GetChild(i).GetComponent<Toggle>().onValueChanged.AddListener(v =>
                {
                    panels.GetChild(index).gameObject.SetActive(v);
                    if (v && onValueChanged != null)
                        onValueChanged(index);
                });
            }

            toggles.GetChild(0).GetComponent<Toggle>().isOn = true;
        }
    }
}

