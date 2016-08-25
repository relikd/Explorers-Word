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

	override public void OnInteractionKeyPressed() {
		if (triggerScript)
			triggerScript.OnTriggerInteraction (this);
		scriptedActionExecuter (responseMessage);
		centeredMessage (responseMessage);
	}

	bool scriptedActionExecuter (string src) {
		if (src.Contains ("[leave_room]")) {
			LevelManager.LoadNextRoom ();
			responseMessage = responseMessage.Replace ("[leave_room]", "");
			return true;
		}
		return false;
	}
}
