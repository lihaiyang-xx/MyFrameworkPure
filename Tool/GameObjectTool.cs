using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class GameObjectTool
    {
        /// <summary>
        /// 查找所有物体
        /// </summary>
        /// <param equimpentName="name"></param>
        /// <returns></returns>
        public static GameObject[] FindGameobjects(string name)
        {
            List<GameObject> goList = new List<GameObject>();
            Transform[] trs = Object.FindObjectsOfType<Transform>();
            foreach (Transform tr in trs)
            {
                if (tr.GetHierarchyPath() == name)
                {
                    goList.Add(tr.gameObject);
                }
            }
            return goList.ToArray();
        }

        /// <summary>
        /// 查找物体（包括非激活状态）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindGameObject(string name)
        {
            Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>();
            Transform tr = trs.FirstOrDefault(x => Regex.IsMatch(x.GetHierarchyPath(),string.Format(@"^(.+/)*{0}$", name)));
            
            return tr ? tr.gameObject : null;
        }
    }
}

