using UnityEngine;
using Interaction;

/**
 * Search for objects which are in reach of the player
 */
public class Reachable : MonoBehaviour
{
	GameObject currentGameObject;
	bool initial = true;
	public float Reach = 0F;

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