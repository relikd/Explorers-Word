using UnityEngine;
using System.Collections;
using Interaction;

/**
* Functionality for the Vent Interaction in Room 3.
*/
public class OpenVentInteraction : PlainInteraction {
	
	[SerializeField]private Interactable[] Screws;
	bool removedAllScrews;
	public string TriggerMessage;
	public string CenteredMessage;

	/**
	 * Gives the response Message.
	 */
	public override string interactMessage () {			
		return responseMessage;
	}

	/**
	 * Checks if all the Screws are removed.
	 */
	void LateUpdate() {
		checkIfAllScrewsAreRemoved ();
	}
		
	/**
	 * Method for checking if the Screws are removed.
	 */
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

	/**
	 * If all screws are removed and an interaction takes place, it loads the next Room .
	 */
	public override void OnInteractionKeyPressed () {
		Debug.Log (Screws.Length);
		if (removedAllScrews) {
			LevelManager.LoadNextRoom ();
		}
	}
}
