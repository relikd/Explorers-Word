using UnityEngine;
using System.Collections;
using Interaction;

public class OpenVentInteraction : PlainInteraction {
	
	[SerializeField]private Interactable[] Screws;
	bool removedAllScrews;
	public string TriggerMessage;
	public string CenteredMessage;

	/**
	 * Removes the assigned {@link PlainInteraction} on initialize		*/
	void Awake() {

	}

	public override string interactMessage () {			
		return responseMessage;
	}

	void LateUpdate() {
		checkIfAllScrewsAreRemoved ();
	}
		
	private void checkIfAllScrewsAreRemoved() {
		int count = 0;
		foreach (Interactable interact in Screws) {
			if (!interact.interactionEnabled) {
				count++;
			} 
		}
		if (count == Screws.Length) {
			removedAllScrews = true;
			responseMessage = TriggerMessage;
			centeredMessage (CenteredMessage);
		}
	}

	public override void OnInteractionKeyPressed () {
		Debug.Log (Screws.Length);
		if (removedAllScrews) {
			LevelManager.LoadNextRoom ();
		}
	}
}
