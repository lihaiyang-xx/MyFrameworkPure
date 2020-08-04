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

    public static void Block(GameObject go)
    {
        Selectable[] selectables = go.GetComponentsInChildren<Selectable>(true);
        selectables.ForEach(x => 
        {
            DragEventBlocker dragEventBlocker = x.gameObject.GetOrAddCompoent<DragEventBlocker>();
            dragEventBlocker.enabled = true;
        });
    }

    public static void RemoveBlock(GameObject go)
    {
        Selectable[] selectables = go.GetComponentsInChildren<Selectable>(true);
        selectables.ForEach(x =>
        {
            DragEventBlocker dragEventBlocker = x.gameObject.GetOrAddCompoent<DragEventBlocker>();
            dragEventBlocker.enabled = false;
        });
    }

    public static void BlockAllScrollRect()
    {
        ScrollRect[] scrollRects = GameObjectTool.FindObjectsOfType<ScrollRect>();
        scrollRects?.ForEach(x =>
        {
            Selectable[] selectables = x.GetComponentsInChildren<Selectable>(true);
            selectables.ForEach(y=>y.gameObject.GetOrAddCompoent<DragEventBlocker>());
        });
    }

    public static void RemoveAllBlock()
    {
        DragEventBlocker[] dragEventBlockers = GameObjectTool.FindObjectsOfType<DragEventBlocker>();
        dragEventBlockers?.DestroyImmediate(true);
    }
}