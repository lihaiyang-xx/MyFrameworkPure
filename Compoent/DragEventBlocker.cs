using Ludiq;
using MyFrameworkPure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEventBlocker : MonoBehaviour
    , IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public static void BlockScrollRect()
    {
        ScrollRect[] scrollRects = GameObjectTool.FindObjectsOfType<ScrollRect>();
        scrollRects.ForEach(x =>
        {
            Selectable[] selectables = x.GetComponentsInChildren<Selectable>(true);
            selectables.ForEach(y =>  y.GetOrAddComponent<DragEventBlocker>());
        });

    }

    public static void ClearBlockScrollRect()
    {
        ScrollRect[] scrollRects = GameObjectTool.FindObjectsOfType<ScrollRect>();
        scrollRects.ForEach(x =>
        {
            DragEventBlocker[] dragEventBlockers = x.GetComponentsInChildren<DragEventBlocker>(true);
            dragEventBlockers.DestroyImmediate(true);
        });
    }


}