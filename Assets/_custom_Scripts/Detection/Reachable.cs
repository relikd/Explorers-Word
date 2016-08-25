using UnityEngine;
using Interaction;
using System;

/**
 * Search for objects which are in reach of the player
 */
public class Reachable : MonoBehaviour
{
	private GameObject currentGameObject;
	public float Reach = 0F;
	public float proceduralReach;

	// PRIVATE SETTINGS
	[HideInInspector] public RaycastHit RaycastHit;

	/**
	 * Cast a ray in each update to see whats in front of the camera
	 * If find any Interaction script enable the GUI respectively
	 */
	void Update()
	{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));
		RaycastHit hit; // Variable reading information about the collider hit.

		proceduralReach = getDownLookingReach (1.9f, 50, 10);
		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, proceduralReach)) {
			RaycastHit = hit;

			GameObject go = hit.transform.gameObject;
			if (currentGameObject != go ) {
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
			currentGameObject = null;
		}
	}
	/**
	 * Gradually increas reach if player is looking down to the floor
	 * @param maxReach will be used if looking down, should be as high as the player/camera (1.9f)
	 * @param startingAngle from which point on the increase should start. 0 - 90, where 90 is down and 0 horizontal
	 * @param graduallyOverDegrees dont jump from Reach to maxReach, increase partly linear
	 * @return the new reach distance
	 */
	private float getDownLookingReach(float maxReach, float startingAngle, float graduallyOverDegrees) {
		float currentAngle = Camera.main.transform.eulerAngles.x;
		if (currentAngle > startingAngle && currentAngle <= 90) {
			float tmp = Camera.main.transform.eulerAngles.x - startingAngle;
			// get reach from settings and add enough to get up to at least 1.9 (the height of the player)
			return Reach + Mathf.Min (tmp / graduallyOverDegrees, 1.0F) * Mathf.Max(maxReach-Reach, 0);
		}
		return Reach;
	}
	/**
	 * Update Cursor if interaction is possible
	 */
	private void activateCrosshair(bool enable) {
		MouseCrosshair ch = gameObject.GetComponent<MouseCrosshair> ();
		if (ch) ch.activateCrosshair(enable);
	}
	/**
	 * Go through all children and deactivate the interaction GUI
	 */
	private void deactiveGUI() {
		if (currentGameObject) {
			Interactable[] goInteraction = currentGameObject.GetComponents<Interactable> ();
			foreach (Interactable i in goInteraction) {
				i.EnableGUI (false);
			}
		}
	}
}