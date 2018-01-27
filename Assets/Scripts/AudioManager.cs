using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {

	DaggerSwish,
	ShieldHit,
}

public class AudioManager : MonoBehaviour {

	//public AudioSource audioSource;

	// Clips
	public AudioClip daggerSwishClip;
	public AudioClip shieldHitClip;


	public Dictionary<AudioType, AudioClip> audioDict = new Dictionary<AudioType, AudioClip>();

	void Awake()
	{
		//audioSource = GetComponent<AudioSource>();

		audioDict.Add(AudioType.DaggerSwish, daggerSwishClip);
		audioDict.Add(AudioType.ShieldHit, shieldHitClip);	
	}

//	void Update ()
//	{
//		if (Input.GetKeyDown(KeyCode.G))
//		{
//							
//		}
//	}

	public void Play(AudioType audioType)
	{
		GameObject go = new GameObject();
		go.AddComponent<AudioSource>();
		AudioSource audioSource = go.GetComponent<AudioSource>();
		audioSource.clip = audioDict[audioType];
		audioSource.playOnAwake = false;
		audioSource.PlayOneShot(audioDict[audioType]);

		Destroy(go, 3.0f);

	}
}
