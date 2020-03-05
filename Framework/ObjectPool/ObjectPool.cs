using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MyFrameworkPure
{
    public class ObjectPool
    {

        private int m_AllocNum = 0;
        private GameObject m_BaseGameObj;
        /// <summary>
        /// 空闲对象列表
        /// </summary>
        private List<GameObject> m_IdleList = new List<GameObject>();
        private int m_ReAllocNum = 0;
        /// <summary>
        /// //正在使用中的对象列表
        /// </summary>
        private List<GameObject> m_UsingList = new List<GameObject>();

        private GameObject poolRoot;

        private string poolName;

        private static List<ObjectPool> poolList = new List<ObjectPool>();

        public ObjectPool(string poolName)
        {
            if (poolList.Find(x => x.poolName == poolName) != null)
            {
                Debug.LogError($"已经存在对象池:{poolName}!");
                return;
            }
            poolRoot = new GameObject(poolName);
            poolList.Add(this);
        }



        public void Init(GameObject baseobj, int allocnum, int reallocnum = 0)
        {
            Debug.Log("eff name:" + baseobj.name);
            this.m_BaseGameObj = baseobj;
            this.m_AllocNum = allocnum;
            this.m_ReAllocNum = reallocnum;
            this.Alloc(this.m_AllocNum);

        }


        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="allocnum"></param>
        public void Alloc(int allocnum)
        {
            for (int i = 0; i < allocnum; i++)
            {
                GameObject item = UnityEngine.Object.Instantiate(this.m_BaseGameObj, Vector3.zero, Quaternion.identity) as GameObject;
                if (item == null)
                {
                    return;
                }
                item.transform.parent = poolRoot.transform;
                item.SendMessage("AllocCall", this, SendMessageOptions.DontRequireReceiver);
                item.SetActive(false);
                this.m_IdleList.Add(item);

            }

        }

        /// <summary>
        /// 回收不在使用的对象
        /// </summary>
        /// <param name="pushobj"></param>
        /// <returns></returns>
        public bool Push(GameObject pushobj)
        {
            if (this.m_UsingList.Find(x => x == pushobj))
            {
                pushobj.SendMessage("DisActive",SendMessageOptions.DontRequireReceiver);
                pushobj.gameObject.SetActive(false);

                this.m_IdleList.Add(pushobj);
                this.m_UsingList.Remove(pushobj);

                return true;
            }

            return false;
        }


        /// <summary>
        /// 返回索引为0的元素
        /// </summary>
        /// <returns></returns>
        public GameObject Pop()
        {
            if (this.m_IdleList.Count == 0)
            {
                if (this.m_ReAllocNum == 0)
                {
                    if (this.m_UsingList.Count > 0)
                    {
                        GameObject obj2 = this.m_UsingList[0];
                        obj2.SendMessage("Active", SendMessageOptions.DontRequireReceiver);
                        this.m_IdleList.Add(obj2);
                        this.m_UsingList.Remove(obj2);
                    }
                }
                else
                {
                    this.Alloc(this.m_ReAllocNum);

                }
            }

            if (this.m_IdleList.Count <= 0)
            {
                return null;
            }

            GameObject item = this.m_IdleList[0];
            item.gameObject.SetActive(true);
            this.m_UsingList.Add(item);
            this.m_IdleList.Remove(item);
            return item;
        }



        public int GetAllocNum()
        {
            return this.m_AllocNum;
        }

        public int GetReAllocNum()
        {
            return this.m_ReAllocNum;
        }


        /// <summary>
        /// 清理对象池
        /// </summary>
        public void Clean()
        {
            for (int i = 0; i < this.m_UsingList.Count; i++)
            {
                UnityEngine.Object.DestroyImmediate(this.m_UsingList[i]);
            }

            for (int j = 0; j < this.m_IdleList.Count; j++)
            {
                UnityEngine.Object.DestroyImmediate(this.m_IdleList[j]);
            }

            UnityEngine.Object.Destroy(poolRoot);

        }

        public static ObjectPool FindPool(string name)
        {
            return poolList.Find(x => x.poolName == name);
        }

        //public static void CleanAll()
        //{
        //    for (var i = 0; i < poolList.Count; i++)
        //    {
        //        ObjectPool objectPool = poolList[i];
        //        ref ObjectPool pool = ref objectPool;
        //        pool.Clean();
        //        pool = null;
        //    }

        //    poolList.Clear();
        //}
    }
}
