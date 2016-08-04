using UnityEngine;
using System.Collections;
using UnityEngine.UI;

	public class Reachable : MonoBehaviour
	{
		public float Reach = 0F;
		//[Tooltip("The tag that triggers the object to be openable")]
		public string TriggerTag = "asdfs";

		// PRIVATE SETTINGS
		[HideInInspector] public bool InReach;
		[HideInInspector] public RaycastHit RaycastHit;

		void Update()
		{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));

		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach))
		{
			RaycastHit = hit;	
			if (hit.collider.tag == TriggerTag) 
			{
				InReach = true;
			} else
				InReach = false;
				
		}else
			InReach = false;
		}
	}

