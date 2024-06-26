﻿using System.Collections;
using System.Collections.Generic;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MyFrameworkPure
{
    /// <summary>
    /// 完整事件监听器
    /// </summary>
    public class EventTriggerListener : BaseEventTriggerListener, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public UnityAction<PointerEventData> onBeginDrag;
        public UnityAction<PointerEventData> onDrag;
        public UnityAction<PointerEventData> onEndDrag;

        public new static EventTriggerListener Get(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(eventData);
        }
    }
}

