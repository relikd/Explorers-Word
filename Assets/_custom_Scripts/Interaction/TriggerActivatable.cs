using UnityEngine;
using System.Collections;
using Interaction;

public class TriggerActivatable : NonInteratable
{
	public bool firstUseActivatesTrigger = false;
	public bool triggerActive = false;
	public Interactable triggerScript;

	override public string interactMessage() {
		if (triggerActive && triggerScript)
			return triggerScript.interactMessage ();
		return actionMessage;
	}

	override public void HandleRaycastCollission() {
		if (triggerActive && triggerScript)
			triggerScript.HandleRaycastCollission ();
		else if (Input.GetKeyUp (theKeyCode())) {
			GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
			if (gm) gm.centeredMessage (responseMessage);

			if (firstUseActivatesTrigger && triggerScript) {
				EnableGUI (false);
				firstUseActivatesTrigger = false;
				triggerActive = true;
			}
		}
	}
}
