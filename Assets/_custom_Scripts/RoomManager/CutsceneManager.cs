using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/** Contains scripts for handling room specific logic */
namespace RoomManager
{
	/**
	 * Dynamically load next Level and display text in between of switch
	 */
	public class CutsceneManager : MonoBehaviour
	{
		/**
		 * Class for saving title, subtitle and audio clip for each cutscene
		 */
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
		private bool prologPlayed = false;
		private float timePassed = 0.0f;

		/** Handles prologue after room 0 and loads start menu after ending scene */
		void Start () {
			if (LevelManager.currentLevel == 0)
				prepareCutscene (0);
			else if (LevelManager.currentLevel + 1 == chapters.Length) {
				title.text = "";
				subtitle.text = "";
				LevelManager.LoadStartScreen ();
			}
			else
				prepareCutscene (LevelManager.currentLevel + 1);
		}
		/** Reset all variables, set text and play audio */
		void prepareCutscene(int index) {
			timePassed = 0.0f;
			audioHasStarted = false;

			if (index < chapters.Length) {
				title.text = chapters [index].title;
				subtitle.text = chapters [index].subtitle;
				StartCutsceneAudio (index);
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
				if (LevelManager.currentLevel == 0 && prologPlayed == false) {
					prologPlayed = true;
					prepareCutscene (1);
				} else {
					LevelManager.LoadRoom (LevelManager.currentLevel + 1);
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
}