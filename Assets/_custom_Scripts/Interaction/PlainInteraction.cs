using UnityEngine;
using System.Collections;
using Interaction;
using UnityEditor;

public class PlainInteraction : Interactable
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
			centeredMessage (responseMessage);
		}
	}
}
