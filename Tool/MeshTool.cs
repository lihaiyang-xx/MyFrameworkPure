using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// 网格工具
    /// </summary>
    public class MeshTool
    {
        public static void UpdateSkinMeshCollider(GameObject go)
        {
            Mesh mesh = new Mesh();
            go.GetComponent<SkinnedMeshRenderer>().BakeMesh(mesh);
            go.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}

