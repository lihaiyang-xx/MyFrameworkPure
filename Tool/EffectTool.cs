using System.Collections;
using System.Collections.Generic;
using HighlightingSystem;
using UnityEngine;
using UnityEngine.Events;

public class EffectTool
{
    public static void DoGrowthEffect(GameObject go, float time)
    {
        Shader stencilMask = Shader.Find("Custom/StencilMask");
        Shader stencilStandard = Shader.Find("Custom/Stencil_Standard");
        if (!stencilMask || !stencilStandard)
        {
            Debug.LogError("没有找到模板测试shader!");
            return;
        }
        MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer>();
        renderers.ForEach((x) =>
        {
            x.materials.ForEach((y) =>
            {
                if (!y.shader.name.Contains("Standard"))
                {
                    Debug.LogWarning(y.name + "不是standard材质,可能产生无法预期的效果上");
                }
                y.shader = stencilStandard;
            });
        });
        Bounds bounds = go.GetBounds();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(cube.GetComponent<Collider>());
        cube.GetComponent<MeshRenderer>().material.shader = stencilMask;
        cube.transform.position = new Vector3(bounds.center.x,bounds.center.y - bounds.size.y,bounds.center.z);
        cube.transform.localScale = bounds.size;

        UnityAction ua = () =>
        {
            cube.transform.Translate(Vector3.up * Time.deltaTime/time);
            if (cube.transform.position.y > bounds.center.y)
            {
                Shader standard = Shader.Find("Standard");
                cube.transform.SetPosY(bounds.center.y);
                renderers.ForEach((x) =>
                {
                    x.materials.ForEach((y) =>
                    {
                        y.shader = standard;
                    });
                });
                
            }
        };

        MonoBehaviorTool.Instance.RegisterUpdate(ua);
    }
	
}
