using UnityEngine;
using System.Collections;
using Interaction;

public class PlainInteraction : Interactable
{
	[Tooltip("Press 'E' to [message]")]
	public string actionMessage = "interact";
	[Multiline]
	public string responseMessage = "Interaction not possible";

	override public string interactMessage() {
		return actionMessage;
	}

	override public void OnInteractionKeyPressed() {
		centeredMessage (responseMessage);
	}
}
