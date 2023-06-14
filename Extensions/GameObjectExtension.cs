using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyFrameworkPure
{
    /// <summary>
    /// 游戏物体扩展类
    /// </summary>
    public static class GameObjectExtension
    {
        /// <summary>
        /// 判断鼠标是否在游戏物体上
        /// </summary>
        /// <param name="go"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsOnMouse(this GameObject go, Camera camera)
        {
            if (!camera)
            {
                Debug.LogError("没有合适的相机！");
                return false;
            }
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                if (hitInfo.transform.gameObject.Equals(go) || hitInfo.transform.IsChildOf(go.transform))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否点击到物体
        /// </summary>
        /// <param name="go"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool IsOnMouseDown(this GameObject go, Camera camera)
        {
            return Input.GetMouseButton(0) && go.IsOnMouse(camera);
        }

        /// <summary>
        /// 获取物体的边界框
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static Bounds GetBounds(this GameObject go)
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
            renderers.ForEach((x) =>
            {
                if (bounds.size == Vector3.zero)
                {
                    bounds = x.bounds;
                }
                bounds.Encapsulate(x.bounds);
            });
            return bounds;
        }

        /// <summary>
        /// 获取或添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T t = go.GetComponent<T>();
            if (!t)
                t = go.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// 获取子物体组件,排除自身
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject go) where T : Component
        {
            T[] componentsInChildren = go.GetComponentsInChildren<T>();
            return componentsInChildren.Where(c => !ReferenceEquals(c.gameObject, go)).ToArray();
        }

        /// <summary>
        /// 用于在游戏对象的父级中获取指定类型的基础组件（不包括派生类）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static Component GetBaseComponentInParent<T>(this GameObject go) where T:Component
        {
            T[] components = go.GetComponentsInParent<T>();
            return components.FirstOrDefault(x => x.GetType() == typeof(T)) as Component;
        }

        /// <summary>
        /// 获取子物体的所有材质（包括自身）
        /// </summary>
        /// <param name="go"></param>
        /// <param name="predicate"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public static Material[] GetMaterialsInChildren(this GameObject go, bool includeInactive = false, Predicate<Material> predicate = null)
        {
            List<Material> matlist = new List<Material>();
            MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive);
            renderers.ForEach(x =>
            {
                Material[] mats = Application.isPlaying ? x.materials : x.sharedMaterials;
                mats.ForEach(m =>
                {
                    if (predicate != null && predicate(m))
                    {
                        matlist.Add(m);
                    }
                });
            });
            return matlist.ToArray();
        }

        /// <summary>
        /// 反转Active
        /// </summary>
        /// <param name="go"></param>
        public static void InverseActive(this GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}

