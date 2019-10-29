using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
