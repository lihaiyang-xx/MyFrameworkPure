using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

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
		
		/// <summary>
        /// 查找物体(要求根物体处于激活状态)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindGameObjectQuick(string name)
        {
            int index = name.IndexOf('/');
            string rootName = name.Substring(0, index);

            GameObject rootGameObject = GameObject.Find(rootName);
            if (!rootGameObject)
                return null;
            Transform target = rootGameObject.transform.Find(name.Substring(index + 1));
            if (target)
                return target.gameObject;
            else
                return null;
        }
        public static T FindObjectOfType<T>() where  T:Component
        {
            T[] trs = Resources.FindObjectsOfTypeAll<T>();
            return trs != null && trs.Length != 0 ? trs[0] : null;
        }

        public static T[] FindObjectsOfType<T>() where T : Component
        {
            return Resources.FindObjectsOfTypeAll<T>();
        }

        private static Dictionary<Type, Component> cacheDictionary;

        public static T FindTypeWithCache<T>() where T : Component
        {
            if (cacheDictionary == null)
                cacheDictionary = new Dictionary<Type, Component>();
            Type t = typeof(T);
            if (cacheDictionary.ContainsKey(t))
            {
                return cacheDictionary[t] as T;
            }

            return GameObjectTool.FindObjectOfType<T>();
        }
    }
}

