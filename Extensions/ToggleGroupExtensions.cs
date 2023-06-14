using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace MyFrameworkPure
{
    /// <summary>
    /// ToggleGroup扩展
    /// </summary>
    public static class ToggleGroupExtensions
    {

        private static System.Reflection.FieldInfo _toggleListMember;

        /// <summary>
        /// 获取组中的所有Toggle
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        public static IList<Toggle> GetToggles(this ToggleGroup grp)
        {
            if (_toggleListMember == null)
            {
                _toggleListMember = typeof(ToggleGroup).GetField("m_Toggles", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (_toggleListMember == null)
                    throw new System.Exception("UnityEngine.UI.ToggleGroup source code must have changed in latest version and is no longer compatible with this version of code.");
            }
            return _toggleListMember.GetValue(grp) as IList<Toggle>;
        }

        /// <summary>
        /// 组中所有Toggle数量
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        public static int Count(this ToggleGroup grp)
        {
            return GetToggles(grp).Count;
        }

        /// <summary>
        /// 通过索引值获取Toggle
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Toggle Get(this ToggleGroup grp, int index)
        {
            return GetToggles(grp)[index];
        }

        /// <summary>
        /// 设置特定索引的Toggle开关状态
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="index"></param>
        public static void SetActiveToggleByIndex(this ToggleGroup grp, int index)
        {
            IList<Toggle> toggles = GetToggles(grp);
            toggles = toggles.OrderBy(x => x.transform.GetSiblingIndex()).ToList();
            grp.SetAllTogglesOff();
            toggles[index].isOn = true;
        }

        /// <summary>
        /// 设置特定名称的Toggle开关状态
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="name"></param>
        public static void SetActiveToggleByName(this ToggleGroup grp, string name)
        {
            IList<Toggle> toggles = GetToggles(grp);
            grp.SetAllTogglesOff();
            toggles.First(x => x.name == name).isOn = true;
        }

        /// <summary>
        /// 获取打开状态Toggle的索引
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        public static int GetActiveIndex(this ToggleGroup grp)
        {
            Toggle activeToggle = grp.ActiveToggles().FirstOrDefault();
            if (!activeToggle)
                return -1;
            List<Toggle> toggles = grp.GetToggles().OrderBy(x => x.transform.GetSiblingIndex()).ToList();
            return toggles.IndexOf(activeToggle);
        }

    }
}
