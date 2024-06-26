﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using MyFrameworkPure;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 资源监视器
    /// </summary>
    public class AssetImportMonitor : AssetPostprocessor
    {
        public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string assetPath in importedAsset)
            {
#if EXCEL
            if (assetPath.EndsWith(".xls"))
            {
                string fileName = Path.GetFileNameWithoutExtension(assetPath);
                string csvPath = $"Assets/StreamingAssets/Csv/{fileName}.csv";
                ExcelTool.Excel2Csv(assetPath,csvPath);
                AssetDatabase.ImportAsset(csvPath);
                Debug.Log("excel to csv:"+assetPath);
                AssetDatabase.Refresh();
            }
#endif
                if (assetPath.StartsWith("Assets/Arts/UI"))
                {
                    TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter; ;
                    if (textureImporter != null && textureImporter.textureType != TextureImporterType.Sprite)
                    {
                        textureImporter.textureType = TextureImporterType.Sprite;
                        AssetDatabase.ImportAsset(assetPath);
                    }
                }
            }
        }
    }
}

