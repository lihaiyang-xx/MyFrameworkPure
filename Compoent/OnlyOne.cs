using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保证场景中物体唯一
/// </summary>
public class OnlyOne : MonoBehaviour
{
    private static OnlyOne instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            DestroyImmediate(gameObject);
    }
}
