using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerListener : BaseEventTriggerListener, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onEndDrag;
    public UnityAction<PointerEventData.InputButton, Vector3> onGlobalMouseDown;


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
