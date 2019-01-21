using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectToolTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EffectTool.DoGrowthEffect(GameObject.Find("zhengjiqi").gameObject,3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
