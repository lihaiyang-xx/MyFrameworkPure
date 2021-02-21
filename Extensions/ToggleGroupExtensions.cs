using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace MyFrameworkPure
{
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

        public static void SetActiveToggleByIndex(this ToggleGroup grp, int index)
        {
            IList<Toggle> toggles = GetToggles(grp);
            toggles = toggles.OrderBy(x => x.transform.GetSiblingIndex()).ToList();
            grp.SetAllTogglesOff();
            toggles[index].isOn = true;
        }

        public static void SetActiveToggleByName(this ToggleGroup grp, string name)
        {
            IList<Toggle> toggles = GetToggles(grp);
            grp.SetAllTogglesOff();
            toggles.First(x => x.name == name).isOn = true;
        }

    }
}
