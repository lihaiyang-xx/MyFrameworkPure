using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITool
{

    public static bool WereAnyUiElementsHovered()
    {
        if (EventSystem.current == null) return false;

        Vector2 inputDevPos = Input.mousePosition;

        PointerEventData eventDataCurrentPosition =
            new PointerEventData(EventSystem.current) {position = new Vector2(inputDevPos.x, inputDevPos.y)};

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count != 0;
    }
}
