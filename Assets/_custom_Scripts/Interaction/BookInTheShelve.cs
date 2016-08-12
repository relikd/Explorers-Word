using System;
using System.Collections;
using UnityEngine;
using Interaction;


public class BookInTheShelve : MonoBehaviour, Interactable 
{	
	
	private bool shouldDisplayText;

	public void HandleRaycastCollission() {
		if (Input.GetKeyUp (KeyCode.E)) {
			if (Room1Manager.puzzleSolved) {
				//DO STUFF
				playAnimation ();
			} else {
				// display message: "This book cannot be moved. It is fixed in the shelf somehow."
			}
		}
	}

	private void playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		if (anim) {
			anim.SetBool ("Trigger", true);

		}
	}

	public void EnableGUI(bool enable) {
		shouldDisplayText = enable && Room1Manager.puzzleSolved;
	}

	void OnGUI() {
		if (shouldDisplayText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), "Press 'E' to pull");
		}
	}
}

