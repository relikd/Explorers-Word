using UnityEngine;
using System.Collections;

/**
* Manages global Sounds like Background Music. 
*/
public class GlobalSoundPlayer : MonoBehaviour {

	[SerializeField]private AudioClip storytellerAudio;
	[SerializeField][Range(0,1.0f)]private float storytellerVolume = 1.0f;
	[SerializeField]private AudioClip backgroundmusic;
	[SerializeField][Range(0,1.0f)]private float backgroundmusicVolume = 0.15f;
	[SerializeField]private AudioClip[] otherScenesounds;
	[SerializeField][Range(0,1.0f)]private float otherSoundsVolume;

	private AudioSource StoryTellerAudioSource;
	private AudioSource BackgroundAudioSource;
	private AudioSource alternateAudioSource;
	private static AudioSource correctWord;
	private static AudioClip[] correctWordClip;
	private static AudioSource puzzleSolved;

	/**
	* Instantiates nesseccary Variables. 
	*/
	void Awake () {
		StoryTellerAudioSource = gameObject.AddComponent<AudioSource> ();
		BackgroundAudioSource = gameObject.AddComponent<AudioSource> ();
		StoryTellerAudioSource.playOnAwake = false;
		BackgroundAudioSource.playOnAwake = false;

		correctWord = gameObject.AddComponent<AudioSource> ();
		puzzleSolved = gameObject.AddComponent<AudioSource> ();

		correctWord.loop = puzzleSolved.loop = false;
		correctWord.playOnAwake = puzzleSolved.playOnAwake = false;

		correctWordClip = new AudioClip[6];
		Object[] resources = Resources.LoadAll ("objectVisibleSounds");

		for (int i = 0; i < 6; i++) {
			correctWordClip [i] = resources [i] as AudioClip;
		}
		puzzleSolved.clip = Resources.Load ("puzzleSolvedSound") as AudioClip;

		StartAudio ();
	}
	
	/**
	* Updates the Volumes. 
	*/
	void Update () {	
		if (!StoryTellerAudioSource.isPlaying) {
			storytellerVolume = 0.0f;
		}
		StoryTellerAudioSource.volume = storytellerVolume;
		BackgroundAudioSource.volume = backgroundmusicVolume;
	}

	/**
	* Starts Audio Source. 
	*/
	public void StartAudio(){
		StartStoryTellerAudio ();
		//LowerVolumesExceptStoryVolume ();
		StartBackgroundMusic ();
	}

	/**
	* Toggle the Pausing of a given audio source. 
	*/
	public void TogglePauseAudio(AudioSource audioSource){
		if (audioSource.isPlaying) {
			audioSource.Pause ();
		} else{
			audioSource.UnPause();
		}
	}

	/**
	* Stops and restarts a given AudioSource. 
	*/
	public void RestartAudio(AudioSource audioSource)
	{
		audioSource.Stop ();
		StartAudio ();
	}
		
	/**
	* Plays another sound from the other sounds source. 
	*/
	public void PlayOtherSceneSound(int index, bool looped = false, float volume = 1.0f, bool overlappingSound = false){
		if (otherScenesounds.Length != 0) {
			otherSoundsVolume = volume;
			if (overlappingSound) {
				alternateAudioSource = gameObject.AddComponent<AudioSource> ();
				alternateAudioSource.clip = otherScenesounds [index];
				alternateAudioSource.loop = looped;
				alternateAudioSource.volume = otherSoundsVolume;
				alternateAudioSource.Play ();
			} 
		}
	}

	/**
	* Starts the Story Music. 
	*/
	private void StartStoryTellerAudio(){
		if (storytellerAudio != null) {
			StoryTellerAudioSource.clip = storytellerAudio;
			StoryTellerAudioSource.volume = storytellerVolume;
			StoryTellerAudioSource.loop = false;
			StoryTellerAudioSource.Play ();
		}
	}

	/**
	* Starts the Background Music. 
	*/
	private void StartBackgroundMusic(){
		if (backgroundmusic != null) {
			BackgroundAudioSource.clip = backgroundmusic;
			BackgroundAudioSource.volume = backgroundmusicVolume;
			BackgroundAudioSource.loop = true;
			BackgroundAudioSource.Play ();
		}
	}


	/**
	* Plays the CorrectWord Sound. 
	*/
	public static void playCorrectWord() {
		int n = UnityEngine.Random.Range(1, correctWordClip.Length);
		correctWord.clip = correctWordClip [n];
		correctWord.Play ();
		// move picked sound to index 0 so it's not picked next time
		AudioClip currentClip = correctWordClip[n];
		correctWordClip[n] = correctWordClip[0];
		correctWordClip [0] = currentClip;

		//		correctWord.clip = correctWordClip;
		//		correctWord.volume = 1.0f;
		//		correctWord.Play ();
	}

	/**
	* Plays the PuzzleRidle Sound. 
	*/
	public static void playPuzzleSolved() {
		puzzleSolved.volume = 0.8f;
		puzzleSolved.Play ();	
	}

	/**
	* Lower Volumes when Explorers Story is told. 
	*/
	private void LowerVolumesExceptStoryVolume() {
		this.otherSoundsVolume = 0.8f;
		this.backgroundmusicVolume = 0.8f;
	}
		


}
