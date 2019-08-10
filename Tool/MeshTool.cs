using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTool
{
    public static void UpdateSkinMeshCollider(GameObject go)
    {
        Mesh mesh = new Mesh();
        go.GetComponent<SkinnedMeshRenderer>().BakeMesh(mesh);
        go.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
	
}
