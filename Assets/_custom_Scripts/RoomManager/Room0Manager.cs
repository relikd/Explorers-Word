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

	private GlobalSoundPlayer globalsSoundPlayer;

	GameObject player, door, particleBeam;

	/**
	* Fades in Scene and intantiates all Class Variables. 
	*/
	void Start () {
		StartCoroutine(SceneFadeIn());
		player = GameObject.Find ("FirstPersonCharacter");
		door = GameObject.Find ("animated_door");
		particleBeam = GameObject.Find ("doorIndicationBeam");
		globalsSoundPlayer = gameObject.GetComponent<GlobalSoundPlayer> ();
		globalsSoundPlayer.StartAudio ();
		globalsSoundPlayer.PlayOtherSceneSound (0, true, 1.0f, true);
	}
	
	/**
	* Checks if player is near the Door then opens the door. When Player is over a Specific Distance the door is shown.
	*/
	void Update ()
	{
		if (!doorLeft)
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorLeft = distance > 25;
			door.SetActive (doorLeft);
			particleBeam.SetActive (doorLeft);
		}
		else if (!doorVisited)
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorVisited = distance < 5;
			// open door
			Animator anim = door.GetComponent<Animator> ();
			if (anim) anim.SetBool ("open", doorVisited);
		}
	}

	/**
	* Fades in Scene and Waits for a short Time. 
	*/
	IEnumerator SceneFadeIn(){
		float fadeTime = gameObject.GetComponent<SceneFadingScript> ().BeginFade (-1);
		yield return new WaitForSeconds (fadeTime);
	}


}
