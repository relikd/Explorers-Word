using UnityEngine;
using System.Collections;
using Interaction;
using UnityEditor;

public class NonInteractable : Interactable
{
	[Tooltip("Press 'E' to [message]")]
	public string actionMessage = "interact";
	[Multiline]
	public string responseMessage = "Interaction not possible";

	override public string interactMessage() {
		return actionMessage;
	}

	override public void HandleRaycastCollission() {
		if (Input.GetKeyUp (theKeyCode())) {
			GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
			if (gm) gm.centeredMessage (responseMessage);
		}
	}
}
