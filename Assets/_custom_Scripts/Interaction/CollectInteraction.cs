using UnityEngine;
using System.Collections;
using Interaction;

public class CollectInteraction : Interactable
{
	public bool shouldPickUpStatic = false;

	override public string interactMessage() {
		return "take";
	}

	override public void HandleRaycastCollission() {
		gameObject.isStatic = !shouldPickUpStatic;

		if (Input.GetKeyUp (theKeyCode())) {
			this.gameObject.SetActive(false);
		}
	}
}