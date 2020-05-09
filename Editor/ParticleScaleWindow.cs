using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 粒子缩放窗口
/// </summary>
public class ParticleScaleWindow : EditorWindow
{
    private GameObject particleGO;
    private float scaleFactor = 1;
    [MenuItem("Tools/粒子缩放窗口")]
    static void InitWindow()
    {
        ParticleScaleWindow window = GetWindow<ParticleScaleWindow>();
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("需要缩放的粒子");
        particleGO = EditorGUILayout.ObjectField(particleGO, typeof(GameObject),true) as GameObject;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("缩放");
        scaleFactor = EditorGUILayout.FloatField(scaleFactor);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("缩放") && particleGO != null)
        {
            ScaleParticleSystem(particleGO,scaleFactor);
        }
    }
    public void ScaleParticleSystem(GameObject gameObj,float scale)
    {
        var hasParticleObj = false;
        var particles = gameObj.GetComponentsInChildren<ParticleSystem>(true);
        var max = particles.Length;
        for (int idx = 0; idx < max; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            hasParticleObj = true;
            particle.startSize *= scale;
            particle.startSpeed *= scale;
            particle.startRotation *= scale;
            particle.transform.localScale *= scale;
        }
        if (hasParticleObj)
        {
            gameObj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
