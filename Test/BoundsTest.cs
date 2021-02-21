using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFrameworkPure;
public class BoundsTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    

    }

    private void OnDrawGizmos()
    {
        Bounds bounds = gameObject.GetBounds();
        Debug.Log(bounds);
        Gizmos.DrawWireCube(bounds.center,bounds.size);
    }

}
