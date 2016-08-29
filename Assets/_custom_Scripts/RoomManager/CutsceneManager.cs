using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour {

	public AudioClip cutsceneAudioClip;
	private AudioSource audioSource;
	private bool audioHasStarted = false;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> ();
		audioSource.clip = cutsceneAudioClip;
		audioSource.loop = false;
		audioSource.volume = 1.0f;
		audioSource.Play ();

		audioHasStarted = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (audioHasStarted && !audioSource.isPlaying) {
			LevelManager.LoadRoom ("room_1");
		}
	}
}
