using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTool : MonoBehaviour
{
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

    public static void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
