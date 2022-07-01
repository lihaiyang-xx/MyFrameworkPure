using MyFrameworkPure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    /// <summary>
    /// 在VR交互中,用射线点击滚动框里的按钮时,由于手部的轻微抖动,可能会触发滚动框滚动,导致按钮点击无效;将此脚本添加到按钮上可以避免此问题
    /// </summary>
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
                selectables.ForEach(y => y.gameObject.GetOrAddCompoent<DragEventBlocker>());
            });
        }

        public static void RemoveAllBlock()
        {
            DragEventBlocker[] dragEventBlockers = GameObjectTool.FindObjectsOfType<DragEventBlocker>();
            dragEventBlockers?.DestroyImmediate(true);
        }
    }
}
