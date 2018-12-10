using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavTest : MonoBehaviour
{

    public AudioSource audioSource;
	// Use this for initialization
	void Start ()
	{

	    AudioClip clip = WavTool.ToAudioClip(Application.streamingAssetsPath + "/Audio/在冰块上操作.wav");
	    audioSource.clip = clip;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
