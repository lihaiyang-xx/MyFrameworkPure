
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    [RequireComponent(typeof(Text))]
    public class ScrollText : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 0.5f;

        private Text _label;

        private int _length;
        private int _index;

        private string _sourceText;

        // Use this for initialization
        void Awake()
        {
            _label = GetComponent<Text>();
            _sourceText = _label.text;
            //_length = Mathf.FloorToInt(GetComponent<RectTransform>().sizeDelta.x / _label.fontSize);
            _length = Mathf.FloorToInt(GetComponent<RectTransform>().rect.width / _label.fontSize);
        }

        // Update is called once per frame
        void UpdateText()
        {
            //if (_index > _sourceText.Length - _length)
            //    _index = 0;
            //_label.text = _sourceText.Substring(_index,_length);
            //_index++;
            _label.text = _index + _length > _sourceText.Length ? _sourceText.Substring(_index) : _sourceText.Substring(_index, _length);
            _index++;
            _index = _index >= _sourceText.Length ? 0 : _index;
        }

        public void ActiveScrollText()
        {
            if (_sourceText.Length <= _length)
                return;
            _label.alignment = TextAnchor.MiddleLeft;
            if (!IsInvoking("UpdateText"))
                InvokeRepeating("UpdateText", 0, scrollSpeed);
        }

        public void StopScrollText()
        {
            if (IsInvoking("UpdateText"))
                CancelInvoke("UpdateText");
            _label.alignment = TextAnchor.MiddleCenter;
            _label.text = _sourceText;
        }

        public string SourceText
        {
            get { return _sourceText; }
            set
            {
                _sourceText = value;
                _label.text = _sourceText;
                _index = 0;
            }
        }
    }
}

