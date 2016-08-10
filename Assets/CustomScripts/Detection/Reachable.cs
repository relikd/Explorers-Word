using UnityEngine;
using System.Collections;
using UnityEngine.UI;

	public class Reachable : MonoBehaviour
	{
		public float Reach = 0F;
		//[Tooltip("The tag that triggers the object to be openable")]
		//public string TriggerTag = "asdfs";

		// PRIVATE SETTINGS
		[HideInInspector] public bool InReach;
		[HideInInspector] public RaycastHit RaycastHit;

		void LateUpdate()
		{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));
		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach))
		{
			RaycastHit = hit;	
			InReach = true;

			GameObject go = hit.transform.gameObject;
			// Get access to the 'DoorOpening' script attached to the door that was hit.
			Interactable goInteraction = go.GetComponent<Interactable>();

			goInteraction.EnableGUI();

			if (Input.GetKeyUp (KeyCode.E)) {
				goInteraction.HandleRaycastCollission ();
			}
				
		}else
			InReach = false;
		}
	}

