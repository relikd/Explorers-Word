using UnityEngine;
using System.Collections;
using Interaction;

public abstract class InteractionTrigger : MonoBehaviour {
	abstract public void OnTriggerInteraction (TriggerInteractable trigger);
}

public class TriggerInteractable : NonInteractable
{
	public bool triggerActive = false;
	public InteractionTrigger triggerScript;

	override public string interactMessage() {
		return actionMessage;
	}

	override public void HandleRaycastCollission() {
		if (Input.GetKeyUp (theKeyCode())) {
			triggerScript.OnTriggerInteraction (this);
			centeredMessage (responseMessage);
		}
	}
}
