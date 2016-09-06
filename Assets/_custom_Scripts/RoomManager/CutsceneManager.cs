using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour {

	public AudioClip cutsceneAudioClip;
	private AudioSource audioSource;
	private bool audioHasStarted = false;

//	public static int CutsceneIndex 
//	{
//		get;
//		set;
//	}

	void Start () 
	{
		StartCutsceneAudio ();
	}
	
	// Update is called once per frame
	void Update () {
		if (audioHasStarted 
			&& !audioSource.isPlaying
			&& cutsceneAudioClip != null) {
			LevelManager.LoadRoom ("room_1");
		}
	}

	void StartCutsceneAudio()
	{
		if (cutsceneAudioClip != null) {
			audioSource = gameObject.AddComponent<AudioSource> ();
			audioSource.clip = cutsceneAudioClip;
			audioSource.loop = false;
			audioSource.volume = 1.0f;
			audioSource.Play ();

			audioHasStarted = true;
		}
	}
}
