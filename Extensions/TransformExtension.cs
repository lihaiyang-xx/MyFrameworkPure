using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MyFrameworkPure
{
    public static class TransformExtension
    {
        /// <summary>
        /// 设置位置坐标分量
        /// </summary>
        /// <param name="t"></param>
        /// <param name="pivot"></param>
        /// <param name="v"></param>
        public static void SetPos(this Transform t, char pivot, float v)
        {
            Vector3 pos = t.position;
            switch (Char.ToLower(pivot))
            {
                case 'x':
                    pos.x = v;
                    break;
                case 'y':
                    pos.y = v;
                    break;
                case 'z':
                    pos.z = v;
                    break;
                default:
                    Debug.Log("位置轴参数不正确!");
                    break;
            }

            t.position = pos;
        }
        /// <summary>
        /// 设置世界坐标位置x
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        public static void SetPosX(this Transform t, float x)
        {
            Vector3 v = t.position;
            v.x = x;
            t.position = v;
        }

        /// <summary>
        /// 设置本地坐标分量x
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        public static void SetLocalPosX(this Transform t, float x)
        {
            Vector3 v = t.localPosition;
            v.x = x;
            t.localPosition = v;
        }

        /// <summary>
        /// 设置世界坐标分量y
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        public static void SetPosY(this Transform t, float y)
        {
            Vector3 v = t.position;
            v.y = y;
            t.position = v;
        }

        /// <summary>
        /// 设置本地坐标分量y
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        public static void SetLocalPosY(this Transform t, float y)
        {
            Vector3 v = t.localPosition;
            v.y = y;
            t.localPosition = v;
        }

        /// <summary>
        /// 设置世界坐标分量z
        /// </summary>
        /// <param name="t"></param>
        /// <param name="z"></param>
        public static void SetPosZ(this Transform t, float z)
        {
            Vector3 v = t.position;
            v.z = z;
            t.position = v;
        }

        /// <summary>
        /// 设置本地坐标分量z
        /// </summary>
        /// <param name="t"></param>
        /// <param name="z"></param>
        public static void SetLocalPosZ(this Transform t, float z)
        {
            Vector3 v = t.localPosition;
            v.z = z;
            t.localPosition = v;
        }

        /// <summary>
        /// 设置本地缩放分量x
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        public static void SetLocalScaleX(this Transform t, float x)
        {
            Vector3 v = t.localScale;
            v.x = x;
            t.localScale = v;
        }

        /// <summary>
        /// 设置本地缩放分量y
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        public static void SetLocalScaleY(this Transform t, float y)
        {
            Vector3 v = t.localScale;
            v.y = y;
            t.localScale = v;
        }

        /// <summary>
        /// 设置本地缩放分量z
        /// </summary>
        /// <param name="t"></param>
        /// <param name="z"></param>
        public static void SetLocalScaleZ(this Transform t, float z)
        {
            Vector3 v = t.localScale;
            v.z = z;
            t.localScale = v;
        }

        /// <summary>
        /// 设置世界欧拉角分量x
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        public static void SetEulerX(this Transform t, float x)
        {
            Vector3 v = t.eulerAngles;
            v.x = x;
            t.eulerAngles = v;
        }

        /// <summary>
        /// 设置世界欧拉角分量y
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        public static void SetEulerY(this Transform t, float y)
        {
            Vector3 v = t.eulerAngles;
            v.y = y;
            t.eulerAngles = v;
        }

        /// <summary>
        /// 设置世界欧拉角分量z
        /// </summary>
        /// <param name="t"></param>
        /// <param name="z"></param>
        public static void SetEulerZ(this Transform t, float z)
        {
            Vector3 v = t.eulerAngles;
            v.z = z;
            t.eulerAngles = v;
        }

        /// <summary>
        /// 设置本地欧拉角分量x
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        public static void SetLocalEulerX(this Transform t, float x)
        {
            Vector3 v = t.localEulerAngles;
            v.x = x;
            t.localEulerAngles = v;
        }

        /// <summary>
        /// 设置本地欧拉角分量y
        /// </summary>
        /// <param name="t"></param>
        /// <param name="y"></param>
        public static void SetLocalEulerY(this Transform t, float y)
        {
            Vector3 v = t.localEulerAngles;
            v.y = y;
            t.localEulerAngles = v;
        }

        /// <summary>
        /// 设置本地欧拉角分量z
        /// </summary>
        /// <param name="t"></param>
        /// <param name="z"></param>
        public static void SetLocalEulerZ(this Transform t, float z)
        {
            Vector3 v = t.localEulerAngles;
            v.z = z;
            t.localEulerAngles = v;
        }

        /// <summary>
        /// 获取第level级父物体
        /// </summary>
        /// <param name="self"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Transform GetParentByLevel(this Transform self, int level)
        {
            Transform t = self;
            while (level-- > 0)
            {
                if (t.parent == null)
                {
                    return null;
                }
                t = t.parent;
            }
            return t;
        }

        /// <summary>
        /// 搜索子物体组件
        /// </summary>
        public static T GetComponentInSingleChild<T>(this Transform t, string subnode) where T : Component
        {
            if (t != null)
            {
                Transform sub = t.Find(subnode);
                if (sub != null) return sub.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 扩展方法 查找所有父物体
        /// 若未找到，返回-1
        /// 若找到，返回相隔层数（临近子物体于其父物体相隔1层）
        /// outNearChild表示找到的父物体的与其相隔1层的子物体
        /// </summary>
        /// <param name="self"></param>
        /// <param name="parentName"></param>
        /// <param name="NearChild"></param>
        /// <returns></returns>
        public static int FindParents(this Transform self, string parentName, out Transform NearChild)
        {
            NearChild = null;
            Transform t = self;
            int level = 0;
            while (t.parent != null)
            {
                level++;
                t = t.parent;
                if (t.name == parentName)
                {
                    return level;
                }
                NearChild = t;
            }
            return -1;
        }

        /// <summary>
        /// 清除所有子物体
        /// </summary>
        /// <param name="t"></param>
        /// <param name="immediate"></param>
        /// <param name="exclude"></param>
        public static void ClearChild(this Transform t, bool immediate = false, Predicate<Transform> exclude = null)
        {
            if (!t) return;
            for (int i = t.childCount - 1; i >= 0; i--)
            {
                Transform child = t.GetChild(i);
                if (exclude != null && exclude.Invoke(child))
                    continue;
                if (immediate)
                {
                    UnityEngine.Object.DestroyImmediate(child.gameObject);
                }
                else
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }
        }

        /// <summary>
        /// 复制填充Transform参数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="copy"></param>
        public static void CopyTransform(this Transform t, Transform copy, bool isLocal = true)
        {
            CopyTransformWithoutScale(t, copy, isLocal);
            t.transform.localScale = copy.transform.localScale;

        }

        public static void CopyTransformWithoutScale(this Transform t, Transform copy, bool isLocal = true)
        {
            if (isLocal)
            {
                t.transform.localPosition = copy.transform.localPosition;
                t.transform.localRotation = copy.transform.localRotation;
            }
            else
            {
                t.transform.position = copy.transform.position;
                t.transform.rotation = copy.transform.rotation;
            }
        }

        /// <summary>
        /// 重置变换
        /// </summary>
        /// <param name="t"></param>
        public static void Reset(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        /// <summary>
        /// 获取子变换,如果超出边界则返回空,不会抛出异常
        /// </summary>
        /// <param name="t"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Transform GetChildOrDefault(this Transform t, int index)
        {
            if (t.childCount <= index)
                return null;
            return t.GetChild(index);
        }

        /// <summary>
        /// 递归查找子物体名称包含match的所有子物体,忽略大小写
        /// </summary>
        /// <param name="t"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Transform[] FindByMatch(this Transform t, string match)
        {
            Transform[] childTransforms = t.GetComponentsInChildren<Transform>();
            IEnumerable<Transform> children = childTransforms.Where((x) => x.name.ToLower().Contains(match.ToLower()));

            return children as Transform[] ?? children.ToArray();
        }

        /// <summary>
        /// 查找第一个名称为name的子物体,大小写敏感
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindFirst(this Transform t, string name)
        {
            Transform[] childTransforms = t.GetComponentsInChildren<Transform>(true);
            return childTransforms.FirstOrDefault((x) => x.name == name);
        }

        /// <summary>
        /// 查找第一个名称为name的子物体,忽略大小写
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindName(this Transform t, string name)
        {
            Transform[] childTransforms = t.GetComponentsInChildren<Transform>(true);
            return childTransforms.FirstOrDefault((x) => x.name.ToLower().Contains(name.ToLower()));
        }

        /// <summary>
        /// 获取符合条件的第一个子物体
        /// </summary>
        /// <param name="t"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Transform GetChild(this Transform t, Predicate<Transform> predicate)
        {
            foreach (Transform child in t)
            {
                if (predicate(child))
                    return child;
            }

            return null;
        }

        /// <summary>
        /// 获取第一个符合条件的索引
        /// </summary>
        /// <param name="t"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int GetChildIndex(this Transform t, Predicate<Transform> predicate)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                if (predicate(t.GetChild(i)))
                    return i;
            }

            return -1;
        }

        public static Transform[] GetChildren(this Transform t)
        {
            Transform[] children = new Transform[t.childCount];
            for (int i = 0; i < t.childCount; i++)
            {
                children[i] = t.GetChild(i);
            }

            return children;
        }

        /// <summary>
        /// 获取符合条件的子变换
        /// </summary>
        /// <param name="t"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Transform[] GetChildren(this Transform t, Predicate<Transform> predicate)
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                if (predicate != null && predicate(child))
                    list.Add(child);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取激活的子变换
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Transform[] GetActiveChildren(this Transform t)
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                if (child.gameObject.activeSelf)
                    list.Add(child);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取层级路径
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetHierarchyPath(this Transform t)
        {
            string str = t.name;
            while (t.parent)
            {
                str = str.Insert(0, t.parent.name + "/");
                t = t.parent;
            }
            return str;
        }
        
        /// <summary>
        /// 遍历子物体
        /// </summary>
        /// <param name="t"></param>
        /// <param name="action"></param>
        public static void TraversalChild(this Transform t, UnityAction<Transform> action)
        {
            foreach (Transform child in t)
            {
                action(child);
            }
        }
    }
}
