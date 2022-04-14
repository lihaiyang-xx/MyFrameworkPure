using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onDrag;
    public VoidDelegate onSelect;
    public VoidDelegate onDeselect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onEndDrag;
    public VoidDelegate onPressed;
    public VoidDelegate onLongPressed;

    public delegate void DataDelegate(GameObject go, PointerEventData e);
    public DataDelegate onClick_Data;
    public DataDelegate onDown_Data;
    public DataDelegate onUp_Data;
    public DataDelegate onDrag_Data;
    public DataDelegate onBeginDrag_Data;

    private bool pressed;
    private float pressTimeCount;
    private const float LongPressedTimeThreshold = 0.5f;

    public bool IsPressed => pressed;

    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(gameObject);
        onClick_Data?.Invoke(gameObject, eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke(gameObject);
        onDown_Data?.Invoke(gameObject, eventData);

        pressed = true;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke(gameObject);
        onUp_Data?.Invoke(gameObject, eventData);

        pressed = false;
    }
    public override void OnSelect(BaseEventData eventData)
    {
        onSelect?.Invoke(gameObject);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        onDeselect?.Invoke(gameObject);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag_Data?.Invoke(gameObject,eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(gameObject);
        onDrag_Data?.Invoke(gameObject, eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(gameObject);
    }
    
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        onUpdateSelect?.Invoke(gameObject);
    }

    void Update()
    {
        if (pressed)
        {
            onPressed?.Invoke(gameObject);
            pressTimeCount += Time.deltaTime;
            if(pressTimeCount > LongPressedTimeThreshold)
                onLongPressed?.Invoke(gameObject);

        }
        else
        {
            pressTimeCount = 0;
        }
    }
}
