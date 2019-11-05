﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacePivot
{
    PositiveZ,
    NegativeZ
}

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private bool keepHorizontal;

    [SerializeField] private FacePivot facePivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (keepHorizontal)
            dir.y = 0;
        if (dir == Vector3.zero)
            return;

        switch(facePivot)
        {
            case FacePivot.PositiveZ:
                transform.forward = dir;
                break;
            case FacePivot.NegativeZ:
                transform.forward = -dir;
                break;
        }
    }

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public bool KeepHorizontal
    {
        get => keepHorizontal;
        set => keepHorizontal = value;
    }

    public FacePivot ForwardPivot
    {
        get => facePivot;
        set => facePivot = value;
    }
}
