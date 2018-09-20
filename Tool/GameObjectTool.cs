﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    public static class GameObjectTool
    {
        /// <summary>
        /// 查找所有物体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject[] FindGameobjects(string name)
        {
            List<GameObject> goList = new List<GameObject>();
            Transform[] trs = Object.FindObjectsOfType<Transform>();
            foreach (Transform tr in trs)
            {
                if (tr.name == name)
                {
                    goList.Add(tr.gameObject);
                }
            }
            return goList.ToArray();
        }
    }
}
