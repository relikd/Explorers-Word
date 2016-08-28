using System;
using UnityEngine;
using Interaction;

public class EnableObjectOnInteraction : PlainInteraction
{
	public bool disableSelfAfterUsage;
	public Interactable[] InteractablesToDisable;
	public GameObject[] GameObjects;

	void LateUpdate() {
		if (InteractablesToDisable != null && interactionEnabled) {
			foreach (Interactable inter in InteractablesToDisable) {
				inter.interactionEnabled = false;
			}
		}
	}

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

