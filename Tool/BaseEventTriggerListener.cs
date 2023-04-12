using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BaseEventTriggerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IUpdateSelectedHandler, ISelectHandler
{
    public UnityAction<PointerEventData> onClick;
    public UnityAction<PointerEventData> onDoubleClick;
    public UnityAction<PointerEventData> onDown;
    public UnityAction<PointerEventData> onEnter;
    public UnityAction<PointerEventData> onExit;
    public UnityAction<PointerEventData> onUp;
    public UnityAction<BaseEventData> onSelect;
    public UnityAction<BaseEventData> onUpdateSelect;
    public UnityAction<PointerEventData.InputButton, Vector3> onGlobalMouseDown;
    public UnityAction onLongPressed;
    public UnityAction onPressed;

    public bool IsPressed => pressed;

    private bool pressed;
    private float pressTimeCount;
    private float lastClickTime = -1;

    private const float DoubleClickThreshold = 0.5f;
    private const float LongPressedTimeThreshold = 0.5f;

    public static BaseEventTriggerListener Get(GameObject go)
    {
        BaseEventTriggerListener listener = go.GetComponent<BaseEventTriggerListener>();
        if (listener == null) listener = go.AddComponent<BaseEventTriggerListener>();
        return listener;
    }

    public static void Enable(GameObject go,bool enable)
    {
        BaseEventTriggerListener listener = go.GetComponent<BaseEventTriggerListener>();
        if (listener)
            listener.enabled = enable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData);

        //issue:eventData.clickCount android平台无效,Unity bug?
        //if (eventData.clickCount == 2)
        //{
        //    onDoubleClick?.Invoke();
        //}

        if (Time.timeSinceLevelLoad - lastClickTime < DoubleClickThreshold)
        {
            onDoubleClick?.Invoke(eventData);
            lastClickTime = -1;
        }
        lastClickTime = Time.timeSinceLevelLoad;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke(eventData);
        pressed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke(eventData);
        pressed = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
       onSelect?.Invoke(eventData);
    }
    
    public void OnUpdateSelected(BaseEventData eventData)
    {
        onUpdateSelect?.Invoke(eventData);
    }

    void Update()
    {
        HandlePressed();
        HandleGlobalMouseInput();
    }

    void HandlePressed()
    {
        if (pressed)
        {
            onPressed?.Invoke();
            pressTimeCount += Time.deltaTime;
            if (pressTimeCount > LongPressedTimeThreshold)
                onLongPressed?.Invoke();
        }
        else
        {
            pressTimeCount = 0;
        }
    }

    void HandleGlobalMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onGlobalMouseDown?.Invoke(PointerEventData.InputButton.Left, Input.mousePosition);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            onGlobalMouseDown?.Invoke(PointerEventData.InputButton.Right, Input.mousePosition);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            onGlobalMouseDown?.Invoke(PointerEventData.InputButton.Middle, Input.mousePosition);
        }
    }
}
