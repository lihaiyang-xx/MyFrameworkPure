using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFToolTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    XFTTSTool.Login();
	    XFTTSTool.TextToSpeech("您好", Application.dataPath + "/test.wav");
	    XFTTSTool.Logout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
