using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/**
 * Dynamically load next Level and display text in between
 */
public class CutsceneManager : MonoBehaviour
{
	[Serializable]
	public class CutsceneChapter {
		public string title;
		public string subtitle;
		public AudioClip audioClip;
	}

	public Text title;
	public Text subtitle;
	public CutsceneChapter[] chapters;

	private AudioSource audioSource;
	private bool audioHasStarted = false;
	private float timePassed = 0.0f;

	void Start () {
		prepareCutscene ();
	}
	/** Reset all variables, set text and play audio */
	void prepareCutscene() {
		timePassed = 0.0f;
		audioHasStarted = false;

		if (LevelManager.currentLevel < chapters.Length) {
			title.text = chapters [LevelManager.currentLevel].title;
			subtitle.text = chapters [LevelManager.currentLevel].subtitle;
			StartCutsceneAudio (LevelManager.currentLevel);
		}
	}
	/** Wait till audio playback finishes then load next level */
	void Update () {
		bool shouldLoadNextLevel = false;
		timePassed += Time.deltaTime;

		// wait till audio finishes
		if (audioHasStarted && !audioSource.isPlaying)
			shouldLoadNextLevel = true;
		// or wait at least 3 seconds if no audio provided
		else if (audioHasStarted == false && timePassed > 3.0f)
			shouldLoadNextLevel = true;

		if (shouldLoadNextLevel) {
			if (LevelManager.currentLevel == 0) {
				LevelManager.currentLevel++;
				prepareCutscene ();
			} else {
				LevelManager.LoadNextRoom ();
			}
		}
	}
	/** Start audio playback if clip is available */
	void StartCutsceneAudio(int index)
	{
		if (index < chapters.Length && chapters[index].audioClip != null) {
			audioSource = gameObject.AddComponent<AudioSource> ();
			audioSource.clip = chapters[index].audioClip;
			audioSource.loop = false;
			audioSource.volume = 1.0f;
			audioSource.Play ();

			audioHasStarted = true;
		}
	}
}