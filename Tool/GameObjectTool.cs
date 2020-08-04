using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            float timeCounter = Time.realtimeSinceStartup;

            Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>();
            Transform tr = trs.FirstOrDefault(x => Regex.IsMatch(x.GetHierarchyPath(),string.Format(@"^(.+/)*{0}$", name)));

            float delta = Time.realtimeSinceStartup - timeCounter;
            if (delta > 1)
            {
                Debug.LogError($"FindGameObject 方法执行时间为{delta},大于1秒的查询方法请谨慎使用!");
            }
            return tr ? tr.gameObject : null;
        }

        /// <summary>
        /// 在指定场景中查找物体(包括非激活状态)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static GameObject FindGameObjectInScene(string name,Scene scene)
        {
            float timeCounter = Time.realtimeSinceStartup;
            
            Transform[] trs = Resources.FindObjectsOfTypeAll<Transform>().Where(x=>x.gameObject.scene == scene).ToArray();
            Transform tr = trs.FirstOrDefault(x => Regex.IsMatch(x.GetHierarchyPath(), string.Format(@"^(.+/)*{0}$", name)));

            float delta = Time.realtimeSinceStartup - timeCounter;
            if (delta > 1)
            {
                Debug.LogError($"FindGameObject 方法执行时间为{delta},大于1秒的查询方法请谨慎使用!");
            }

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

        /// <summary>
        /// 查找类型组件,包括激活和非激活组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjectOfType<T>() where  T:Component
        {
            T[] trs = FindObjectsOfType<T>();
            return trs != null && trs.Length != 0 ? trs[0] : null;
        }

        public static Object FindObjectOfType(Type t)
        {
            Object[] objects = Resources.FindObjectsOfTypeAll(t);
            return objects.IsNullOrEmpty() ? null: objects[0];
        }

        public static T[] FindObjectsOfType<T>() where T : Component
        {
            return Resources.FindObjectsOfTypeAll<T>().Where(x=>!string.IsNullOrEmpty(x.gameObject.scene.name)).ToArray();
        }

        public static T[] FindObjectsOfTypeInScene<T>(Scene scene) where T : Component
        {
            return Resources.FindObjectsOfTypeAll<T>().Where(x => x.gameObject.scene == scene).ToArray();
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

        public static void ClearFindCache()
        {
            cacheDictionary.Clear();
        }

        public static GameObject FindGameObjectWithTag(string tag)
        {
            GameObject[] gos = Resources.FindObjectsOfTypeAll<GameObject>().Where(x=>x.tag.Equals(tag)).ToArray();
            return gos.IsNullOrEmpty() ? null : gos[0];
        }

        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            GameObject[] gos = Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.tag.Equals(tag)).ToArray();
            return gos;
        }
    }
}

