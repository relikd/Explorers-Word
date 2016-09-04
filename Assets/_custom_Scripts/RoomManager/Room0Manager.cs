using UnityEngine;
using System.Collections;
using System;

/**
* Manages Room 0. 
*/
public class Room0Manager : MonoBehaviour
{
	private bool doorLeft = false;
	private bool doorVisited = false;

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject door;
	[SerializeField] private GameObject particleBeam;

	private GlobalSoundPlayer globalSoundPlayer;

	/** Scene Fade in */
	void Start () {
		StartCoroutine(SceneFadeIn());
		globalSoundPlayer = gameObject.GetComponent<GlobalSoundPlayer> ();
		globalSoundPlayer.StartAudio ();
		globalSoundPlayer.PlayOtherSceneSound (0, true, 1.0f, true);
	}
	/** Automatically open door when player is near */
	void Update ()
	{
		if (!doorLeft && particleBeam != null) // only used in previous room 0 version
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorLeft = distance > 25;
			door.SetActive (doorLeft);
			particleBeam.SetActive (doorLeft);
		}
		else if (!doorVisited)
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorVisited = distance < 8;
			// open door
			Animator anim = door.GetComponent<Animator> ();
			if (anim) anim.SetBool ("open", doorVisited);
		}
	}
	/**  Fade scene in and wait for a short period */
	IEnumerator SceneFadeIn(){
		float fadeTime = gameObject.GetComponent<SceneFadingScript> ().BeginFade (-1);
		yield return new WaitForSeconds (fadeTime);
	}
	/** Load cutscene when player enters door */
	void OnTriggerEnter(Collider other) {
		LevelManager.LoadRoom ("cutscene");
	}
}
