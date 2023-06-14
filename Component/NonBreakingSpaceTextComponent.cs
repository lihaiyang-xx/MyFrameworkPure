using UnityEngine.UI;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// unity Text 当字符串中带有半角空格，且半角空格后面的字符串内容超过文本剩余显示宽度时，Text组件会将后面的整段文字做换行。
    /// 使用不换行空格替换半角空格
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class NonBreakingSpaceTextComponent : MonoBehaviour
    {
        public static readonly string no_breaking_space = "\u00A0";

        protected Text label;
        // Use this for initialization
        void Awake()
        {
            label = this.GetComponent<Text>();
            label.RegisterDirtyVerticesCallback(OnTextChange);
        }

        public void OnTextChange()
        {
            label.text = label.text.Replace(" ", no_breaking_space);
        }

    }
}
