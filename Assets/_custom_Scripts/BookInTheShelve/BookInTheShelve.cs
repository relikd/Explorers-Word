using System;
using System.Collections;
using UnityEngine;
using Interaction;


public class BookInTheShelve : MonoBehaviour, Interactable 
{	
	
	
	private bool shouldDisplayText;

	public void HandleRaycastCollission() {
		if (Input.GetKeyUp (KeyCode.E)) {
			//DO STUFF
			playAnimation ();

		}
	}

	private void playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		anim.SetBool ("Trigger", true);
	}

	public void EnableGUI(bool enable) {
		shouldDisplayText = enable;
	}

	void OnGUI() {
		if (shouldDisplayText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), "Press 'E' to pull");
		}
	}
}

