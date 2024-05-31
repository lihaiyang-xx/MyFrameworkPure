using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    public class SpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite hoverSprite;

        void Start()
        {
            EventTriggerListener.Get(gameObject).onEnter += d =>
            {
                image.sprite = hoverSprite;
            };

            EventTriggerListener.Get(gameObject).onExit += d =>
            {
                image.sprite = normalSprite;
            };
        }
    }
}

