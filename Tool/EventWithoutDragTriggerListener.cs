using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventWithoutDragTriggerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IUpdateSelectedHandler, ISelectHandler, IEventSystemHandler
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onLongPressed;
    public VoidDelegate onPressed;

    public UnityAction onDoubleClick;

    public delegate void DataDelegate(GameObject go, PointerEventData e);
    public DataDelegate onClick_Data;
    public DataDelegate onDown_Data;
    public DataDelegate onUp_Data;

    private bool pressed;
    private float pressTimeCount;
    private const float LongPressedTimeThreshold = 0.5f;
    private float lastClickTime = -1;

    public bool IsPressed => pressed;

    public static EventWithoutDragTriggerListener Get(GameObject go)
    {
        EventWithoutDragTriggerListener listener = go.GetComponent<EventWithoutDragTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventWithoutDragTriggerListener>();
        return listener;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
        if (onClick_Data != null) onClick_Data(gameObject, eventData);

        if (Time.timeSinceLevelLoad - lastClickTime < 0.5f)
        {
            onDoubleClick?.Invoke();
            lastClickTime = -1;
        }
        lastClickTime = Time.timeSinceLevelLoad;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
        if (onDown_Data != null) onDown_Data(gameObject, eventData);

        pressed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
        if (onUp_Data != null) onUp_Data(gameObject, eventData);

        pressed = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }
    
    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    void Update()
    {
        if (pressed)
        {
            onPressed?.Invoke(gameObject);
            pressTimeCount += Time.deltaTime;
            if (pressTimeCount > LongPressedTimeThreshold)
                onLongPressed?.Invoke(gameObject);
        }
        else
        {
            pressTimeCount = 0;
        }
    }
}
