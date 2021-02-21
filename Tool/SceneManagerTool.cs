using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyFrameworkPure
{
    /// <summary>
    /// 场景管理工具
    /// </summary>
    public class SceneManagerTool : MonoBehaviour
    {
        /// <summary>
        /// 获取所有场景名
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllSceneName()
        {
            List<string> sceneNameList = new List<string>();
            int count = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < count; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
                sceneNameList.Add(sceneName);
            }

            return sceneNameList.ToArray();
        }

        /// <summary>
        /// 重新载入当前场景
        /// </summary>
        public static void ReloadActiveScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

