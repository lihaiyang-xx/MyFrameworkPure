using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceTool
{
    /// <summary>
    /// 载入资源,通过资源路径判断是Resource加载还是Assetbundle加载
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static UnityEngine.Object LoadAssets(string path)
    {
        UnityEngine.Object obj = null;
        if (Path.IsPathRooted(path))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(path);
            if (!ab)
            {
                Debug.LogError("不存在Assetbundle资源:"+path);
            }
            else
            {
                string name = ab.GetAllAssetNames()[0];
                obj = ab.LoadAsset(name);
            }
        }
        else
        {
            obj = Resources.Load(path);
            if (!obj)
            {
                Debug.LogError("Resource下不存在资源:" + path);
            }
        }
        return obj;
    }

    /// <summary>
    /// 直接实例化资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static UnityEngine.GameObject Instantiate(string path, Transform parent = null)
    {
        UnityEngine.Object obj = LoadAssets(path);
        if (obj != null)
        {
            return UnityEngine.Object.Instantiate(obj, parent) as GameObject;
        }
        return null;
    }

    /// <summary>
    /// 直接实例化资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static UnityEngine.GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        UnityEngine.Object obj = LoadAssets(path);
        if (obj != null)
        {
            return UnityEngine.Object.Instantiate(obj, position, rotation, parent) as GameObject;
        }
        return null;
    }

    /// <summary>
    /// 从网络路径异步加载资源
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static IEnumerator InstantiateFromUrl(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle assetbundle = www.assetBundle;
                string name = assetbundle.GetAllAssetNames()[0];
                UnityEngine.Object obj = assetbundle.LoadAsset(name);
                GameObject go = UnityEngine.Object.Instantiate(obj) as GameObject;
                go.name = go.name.Replace("(Clone)", "");
                assetbundle.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }

    }

    /// <summary>
    /// 从网络路径异步加载场景
    /// </summary>
    /// <param name="url"></param>
    /// <param name="loadModel"></param>
    /// <returns></returns>
    public static IEnumerator LoadLevelFromUrl(string url, LoadSceneMode loadModel = LoadSceneMode.Single)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle assetbundle = www.assetBundle;
                //string sceneName = url.Substring(url.LastIndexOf('/') + 1).Replace(".unity3d", "");
                string sceneName = Path.GetFileNameWithoutExtension(url);
                Debug.Log("即将加载场景：" + sceneName);
                SceneManager.LoadScene(sceneName, loadModel);
                yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                assetbundle.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }
}
