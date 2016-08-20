using UnityEngine;
using System.Collections;

public class GlobalSoundPlayer : MonoBehaviour {

	[SerializeField]private AudioClip storytellerAudio;
	[SerializeField][Range(0,1.0f)]private float storytellerVolume;
	[SerializeField]private AudioClip backgroundmusic;
	[SerializeField][Range(0,1.0f)]private float backgroundmusicVolume;
	[SerializeField]private AudioClip[] otherScenesounds;
	[SerializeField][Range(0,1.0f)]private float otherSoundsVolume;


	private AudioSource audioSource;
	private AudioSource alternateAudioSource;

	// Use this for initialization
	void Awake () {
		audioSource = gameObject.AddComponent<AudioSource> ();
		audioSource.playOnAwake = false;
		//storytellerVolume = 1.0f;
		//backgroundmusicVolume = 1.0f;
		//otherSoundsVolume = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (audioSource.clip == storytellerAudio) {
			audioSource.volume = storytellerVolume;
		} else {
			audioSource.volume = backgroundmusicVolume;
		}

		if (audioSource.clip == storytellerAudio && !audioSource.isPlaying) {
			StartBackgroundMusic ();
		}
	}

	public void StartAudio(){
		if (storytellerAudio != null) {
			StartStoryTellerAudio ();
		} else {
			StartBackgroundMusic ();
		}
	}

	public void TogglePauseAudio(){
		if(audioSource.isPlaying){
			audioSource.Pause ();
		}
		else{
			audioSource.UnPause();
		}
	}

	public void RestartAudio()
	{
		audioSource.Stop ();
		StartAudio ();
	}

	public void PlayOtherSceneSound(int index, bool looped = false, float volume = 1.0f, bool overlappingSound = false){
		if (otherScenesounds.Length != 0) {
			otherSoundsVolume = volume;
			if (overlappingSound) {
				alternateAudioSource = gameObject.AddComponent<AudioSource> ();
				alternateAudioSource.clip = otherScenesounds [index];
				alternateAudioSource.loop = looped;
				alternateAudioSource.volume = otherSoundsVolume;
				alternateAudioSource.Play ();
			} else {
				audioSource.clip = otherScenesounds [index];
				audioSource.loop = looped;
				audioSource.volume = otherSoundsVolume;
				audioSource.Play ();
			}
		}
	}

	private void StartStoryTellerAudio(){
		if (storytellerAudio != null) {
			audioSource.clip = storytellerAudio;
			audioSource.volume = storytellerVolume;
			audioSource.loop = false;
			audioSource.Play ();
		}
	}

	private void StartBackgroundMusic(){
		if (backgroundmusic != null) {
			audioSource.clip = backgroundmusic;
			audioSource.volume = backgroundmusicVolume;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}
}
