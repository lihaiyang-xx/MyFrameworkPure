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
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onEndDrag;

    public UnityAction onPressed;

    public delegate void DataDelegate(GameObject go, PointerEventData e);
    public DataDelegate onClick_Data;
    public DataDelegate onDown_Data;
    public DataDelegate onUp_Data;
    public DataDelegate onDrag_Data;
    public DataDelegate onBeginDrag_Data;

    private bool pressed;

    public bool IsPressed => pressed;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
        if (onClick_Data != null) onClick_Data(gameObject, eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
        if (onDown_Data != null) onDown_Data(gameObject, eventData);

        pressed = true;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
        if (onUp_Data != null) onUp_Data(gameObject, eventData);

        pressed = false;
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag_Data?.Invoke(gameObject,eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag(gameObject);
        if (onDrag_Data != null) onDrag_Data(gameObject, eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) onEndDrag(gameObject);
    }
    
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    void Update()
    {
        if(pressed)
            onPressed?.Invoke();
    }
}
