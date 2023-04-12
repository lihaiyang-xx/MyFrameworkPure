using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    /// <summary>
    /// 按钮式开关
    /// </summary>
    public class ButtonToggle : MonoBehaviour
    {
        [SerializeField] private GameObject onGo;
        [SerializeField] private GameObject offGo;

        [SerializeField] private bool isOn;

        public bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                onGo.SetActive(isOn);
                offGo.SetActive(!isOn);
                onValueChanged?.Invoke(isOn);
            }
        }

        public UnityAction<bool> onValueChanged;
        void Awake()
        {
            onGo.SetActive(isOn);
            offGo.SetActive(!isOn);
            EventTriggerListener.Get(gameObject).onClick = d =>
            {
                IsOn = !isOn;
            };
        }

        
    }
}

