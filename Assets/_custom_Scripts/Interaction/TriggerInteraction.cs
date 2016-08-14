using UnityEngine;
using System.Collections;
using Interaction;

public abstract class TriggerInteractionCallback : MonoBehaviour {
	abstract public void OnTriggerInteraction (TriggerInteraction trigger);
}

public class TriggerInteraction : PlainInteraction
{
	public bool triggerActive = false;
	public TriggerInteractionCallback triggerScript;

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
