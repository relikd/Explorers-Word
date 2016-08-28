using System;
using UnityEngine;
using Interaction;

/**
*  Enables a List of Game objects when an interaction takes place. Is Also Able to Disable a List of Interactables and also it self.
*/
public class EnableObjectOnInteraction : PlainInteraction
{
	public bool disableSelfAfterUsage;
	public Interactable[] InteractablesToDisable;
	public GameObject[] GameObjects;

	/**
	 * If the Script should do something and the List of interactables is not null then it disables the interactables.
	 */
	void LateUpdate() {
		if (InteractablesToDisable != null && interactionEnabled) {
			foreach (Interactable inter in InteractablesToDisable) {
				inter.interactionEnabled = false;
			}
		}
	}

	/**
	 * Gives A response and activates the Game Objects. Also Disables itself if it should.
	 */
	public override void OnInteractionKeyPressed () {
		centeredMessage (responseMessage);
		foreach (GameObject go in GameObjects) {
			go.SetActive (true);
		}
		if (disableSelfAfterUsage) {
			this.interactionEnabled = false;
		}
	}
}

