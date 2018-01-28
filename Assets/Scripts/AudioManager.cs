using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {

	DaggerSwish,
	ShieldHit,
	Falling,
	EndingMatch,
	MainMenu,
	LevelMusic,
	EndingTheme,
	PunchHit,
	Dash,
	WindTunnel,
	SwordPickUp

}

public class AudioManager : MonoBehaviour {

	AudioSource mainMenuAudioSource;
	AudioSource matchAudioSource;

	//public AudioSource audioSource;

	// Clips
	public AudioClip daggerSwishClip;
	public AudioClip shieldHitClip;
	public AudioClip fallClip;
	public AudioClip endingmatchClip;
	public AudioClip mainMenuClip;
	public AudioClip levelMusicClip;
	public AudioClip endingThemeClip;
	public AudioClip punchHitClip;
	public AudioClip dashClip;
	public AudioClip windtunnel;
	public AudioClip swordPickup;

	public Dictionary<AudioType, AudioClip> audioDict = new Dictionary<AudioType, AudioClip>();

	void Awake()
	{
		//audioSource = GetComponent<AudioSource>();

		audioDict.Add(AudioType.DaggerSwish, daggerSwishClip);
		audioDict.Add(AudioType.ShieldHit, shieldHitClip);
		audioDict.Add(AudioType.Falling, fallClip);
		audioDict.Add(AudioType.EndingMatch, endingmatchClip);
		audioDict.Add (AudioType.MainMenu, mainMenuClip);
		audioDict.Add (AudioType.LevelMusic, levelMusicClip);
		audioDict.Add (AudioType.EndingTheme, endingThemeClip);
		audioDict.Add (AudioType.PunchHit, punchHitClip);
		audioDict.Add (AudioType.Dash, dashClip);
		audioDict.Add (AudioType.WindTunnel, windtunnel);
		audioDict.Add (AudioType.SwordPickUp, swordPickup);

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

	string mainMenuAudioSourceTag = "MainMenuAudioSource";
	string matchAudioSourceTag = "MatchAudioSource";

	public void PlayMusic(AudioType audioType)
	{
		GameObject go = new GameObject();
		go.AddComponent<AudioSource>();
		AudioSource audioSource = go.GetComponent<AudioSource>();
		audioSource.clip = audioDict[audioType];
		audioSource.playOnAwake = false;
		audioSource.loop = true;
		audioSource.PlayOneShot(audioDict[audioType]);

		if (audioType == AudioType.MainMenu)
		{
			mainMenuAudioSource = audioSource;
			audioSource.tag = mainMenuAudioSourceTag;

			GameObject match = GameObject.FindGameObjectWithTag(matchAudioSourceTag);
			if (match != null)
			{
				Destroy(match);
			}

		}
		else if (audioType == AudioType.LevelMusic)
		{
			matchAudioSource = matchAudioSource;
			audioSource.tag = matchAudioSourceTag;

			GameObject menu = GameObject.FindGameObjectWithTag(mainMenuAudioSourceTag);
			if (menu != null)
			{
				Destroy(menu);
			}
		}
	}

//	public void Stop()
//	{
//		if (mainMenuAudioSource != null)
//			Destroy(mainMenuAudioSource.gameObject);
//		if (matchAudioSource != null)
//			Destroy(matchAudioSource.gameObject);
//	}

	public void PlayMenuMusic()
	{
		//Stop();

		PlayMusic(AudioType.MainMenu);

	}

	public void PlayMatchMusic()
	{
		//Stop();

		PlayMusic(AudioType.LevelMusic);
			
	}

	void OnEnable()
	{
		//GameManager.onStartMenu += PlayMenuMusic;
		//GameManager.onStartMatch += PlayMatchMusic;

	}

	void OnDisable()
	{
		//GameManager.onStartMenu -= PlayMenuMusic;
		//GameManager.onStartMatch -= PlayMatchMusic;


	}
}
