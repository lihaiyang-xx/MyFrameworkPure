using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MyFrameworkPure
{
    /// <summary>
    /// ab包打包器
    /// </summary>
    public class AssetBundleTool : MonoBehaviour
    {
        [MenuItem("Tools/AssetBundle/Windows-无压缩", false, 0)]

        static void BuildUnCompressedForWindows()
        {
#if !UNITY_STANDALONE_WIN
        Debug.LogError("目标平台和当前平台不匹配！");
        return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64);
        }



        [MenuItem("Tools/AssetBundle/Windows-压缩", false, 0)]

        static void BuildForWindows()
        {
#if !UNITY_STANDALONE_WIN
        Debug.LogError("目标平台和当前平台不匹配！");
        return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Tools/AssetBundle/Windows-低压缩", false, 0)]

        static void BuildLowCompressedForWindows()
        {
#if !UNITY_STANDALONE_WIN
        Debug.LogError("目标平台和当前平台不匹配！");
        return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        }


        [MenuItem("Tools/AssetBundle/Android-无压缩", false, 0)]

        static void BuildUnCompressedForAndroid()
        {
#if !UNITY_ANDROID
            Debug.LogError("目标平台和当前平台不匹配！");
            return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);

        }

        [MenuItem("Tools/AssetBundle/Android-低压缩", false, 0)]
        static void BuildLowCompressedForAndroid()
        {
#if !UNITY_ANDROID
            Debug.LogError("目标平台和当前平台不匹配！");
            return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

        }

        [MenuItem("Tools/AssetBundle/Android-压缩", false, 0)]
        static void BuildForAndroid()
        {
#if !UNITY_ANDROID
            Debug.LogError("目标平台和当前平台不匹配！");
            return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        [MenuItem("Tools/AssetBundle/WebGL-压缩", false, 0)]
        static void BuildForWebGL()
        {
#if !UNITY_WEBGL
            Debug.LogError("目标平台和当前平台不匹配！");
            return;
#endif
            DoBuildAssetBundle(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.WebGL);
        }

        static void DoBuildAssetBundle(string outpath, BuildAssetBundleOptions buildOptions, BuildTarget target)
        {
            UnityEngine.Object[] selectionObjs = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.DeepAssets);
            for (int i = 0; i < selectionObjs.Length; i++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("正在打包资源中。。。", $"第{i}个，共{selectionObjs.Length}个", i * 1.0f / selectionObjs.Length))
                {
                    break;
                }
                string path = AssetDatabase.GetAssetPath(selectionObjs[i]);
                if (Directory.Exists(path))//忽略文件夹
                    continue;
                string fileName = Path.GetFileNameWithoutExtension(path);
                AssetBundleBuild abb = new AssetBundleBuild
                {
                    assetBundleName = fileName + ".assetbundle",
                    assetNames = new string[] { path }
                };

                string outPath = Path.Combine(Application.streamingAssetsPath, "Assetbundle/");
                if (!Directory.Exists(outPath))
                    Directory.CreateDirectory(outPath);
                BuildPipeline.BuildAssetBundles(outPath, new AssetBundleBuild[] { abb }, buildOptions, target);
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();

        }
    }
}

