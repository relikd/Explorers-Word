using UnityEngine;
using System.Collections;
using Interaction;

public class BookInTheShelve : Interactable 
{
	override public string interactMessage() {
		return "pull";
	}

	override public void HandleRaycastCollission() {
		if (Input.GetKeyUp (theKeyCode())) {
			if (Room1Manager.puzzleSolved) {
				//DO STUFF
				playAnimation ();
				// TODO: wait till animation finished
				Room1Manager.shelfOpen = true;
			} else {
				GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
				if (gm) gm.centeredMessage ("This book cannot be moved.\nIt is fixed in the shelf somehow.");
			}
		}
	}

	private void playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		if (anim) {
			anim.SetBool ("Trigger", true);
		}
	}

	override public bool shouldDisplayInteraction () {
		return true; // Room1Manager.puzzleSolved; // could be done like that, but I want the message above
	}
}