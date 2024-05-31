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

        /// <summary>
        /// 屏蔽物体拖动
        /// </summary>
        /// <param name="go"></param>
        public static void Block(GameObject go)
        {
            Selectable[] selectables = go.GetComponentsInChildren<Selectable>(true);
            selectables.ForEach(x =>
            {
                DragEventBlocker dragEventBlocker = x.gameObject.GetOrAddComponent<DragEventBlocker>();
                dragEventBlocker.enabled = true;
            });
        }

        /// <summary>
        /// 移除拖动屏蔽
        /// </summary>
        /// <param name="go"></param>
        public static void RemoveBlock(GameObject go)
        {
            Selectable[] selectables = go.GetComponentsInChildren<Selectable>(true);
            selectables.ForEach(x =>
            {
                DragEventBlocker dragEventBlocker = x.gameObject.GetOrAddComponent<DragEventBlocker>();
                dragEventBlocker.enabled = false;
            });
        }

        /// <summary>
        /// 屏蔽所有滚动框拖动
        /// </summary>
        public static void BlockAllScrollRect()
        {
            ScrollRect[] scrollRects = GameObjectTool.FindObjectsOfType<ScrollRect>();
            scrollRects?.ForEach(x =>
            {
                Selectable[] selectables = x.GetComponentsInChildren<Selectable>(true);
                selectables.ForEach(y => y.gameObject.GetOrAddComponent<DragEventBlocker>());
            });
        }

        /// <summary>
        /// 移除所有屏蔽拖动效果
        /// </summary>
        public static void RemoveAllBlock()
        {
            DragEventBlocker[] dragEventBlockers = GameObjectTool.FindObjectsOfType<DragEventBlocker>();
            dragEventBlockers?.DestroyImmediate(true);
        }
    }
}
