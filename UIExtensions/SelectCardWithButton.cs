using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class SelectCardWithButton : MonoBehaviour
    {
        [SerializeField] private Transform cardParent;
        [SerializeField] private Transform panelParent;

        [SerializeField]
        private int currentIndex;

        public UnityAction<int> onValueChanged;
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < cardParent.childCount; i++)
            {
                if (i >= panelParent.childCount)
                    break;
                var index = i;
                EventTriggerListener.Get(cardParent.GetChild(i).gameObject).onClick = g =>
                {
                    panelParent.TraversalChild(x=>x.SetActive(false));
                    panelParent.GetChild(index).gameObject.SetActive(true);
                    if (currentIndex != index)
                    {
                        currentIndex = index;
                        onValueChanged?.Invoke(currentIndex);
                    }
                };
            }
            EventTriggerListener.Get(cardParent.GetChild(currentIndex).gameObject).onClick?.Invoke(null);
        }
    }
}

