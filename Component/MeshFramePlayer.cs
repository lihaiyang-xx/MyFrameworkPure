using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 网格序列播放器
    /// </summary>
    public class MeshFramePlayer : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/匹配mesh序列", false, 0)]
        static void MapMeshFrame()
        {
            GameObject go = Selection.activeGameObject;
            MeshFramePlayer meshFramePlayer = go.GetComponent<MeshFramePlayer>();
            if (meshFramePlayer == null)
                return;
            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(go);
            string prefabPath = AssetDatabase.GetAssetPath(prefab);
            string folderPath = prefabPath.Substring(0, prefabPath.LastIndexOf("/", StringComparison.Ordinal));
            string[] files = Directory.GetFiles(folderPath).Where(x => !x.EndsWith(".meta")).OrderBy(x => x.Length).ToArray();
            List<Mesh> meshList = new List<Mesh>();
            foreach (var file in files)
            {
                Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(file);
                meshList.Add(mesh);
                Debug.Log(file);
            }

            meshFramePlayer.meshes = meshList.ToArray();
        }
#endif
        public Mesh[] meshes;

        [SerializeField] private int frameRate = 24;

        [SerializeField] private GameObject target;

        private int count;

        private MeshFilter meshFilter;
        void Start()
        {
            meshFilter = target.GetComponent<MeshFilter>();
            float frameTime = 1.0f / frameRate;
            InvokeRepeating("UpdateMesh", 0, frameTime);
        }

        // Update is called once per frame
        void UpdateMesh()
        {
            if (count >= meshes.Length)
                return;
            meshFilter.mesh = meshes[count];
            count++;
        }
    }
}

