﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Interaction;

public class Reachable : MonoBehaviour
{
	GameObject currentGameObject;

	bool initial = true;
	public float Reach = 0F;
	//[Tooltip("The tag that triggers the object to be openable")]
	//public string TriggerTag = "asdfs";

	// PRIVATE SETTINGS
	[HideInInspector] public RaycastHit RaycastHit;

	void Update()
	{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));
		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach)) {
			RaycastHit = hit;


			GameObject go = hit.transform.gameObject;

			if (initial) {
				currentGameObject = go;
				initial = false;
			}

			if (currentGameObject == go ) {
				currentGameObject = go;
			} else {
				deactiveGUI();
				activateCrosshair (false);
				currentGameObject = go;
			}

			Interactable[] goInteraction = go.GetComponentsInChildren<Interactable> ();
			foreach (Interactable i in goInteraction) {
				if (i.shouldDisplayInteraction ()) {
					i.EnableGUI (true);
					activateCrosshair (true);
					i.HandleRaycastCollission();
				}
			}
		} else {
			deactiveGUI ();
			activateCrosshair (false);
		}
	}

	private void activateCrosshair(bool enable) {
		MouseCrosshair ch = gameObject.GetComponent<MouseCrosshair> ();
		if (ch) ch.activateCrosshair(enable);
	}

	private void deactiveGUI() {
		if (currentGameObject) {
			Interactable[] goInteraction = currentGameObject.GetComponents<Interactable> ();
			foreach (Interactable i in goInteraction) {
				i.EnableGUI (false);
			}
		}
	}

}
