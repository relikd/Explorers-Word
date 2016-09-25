using UnityEngine;

namespace Interaction
{
	/**
	 * Changes the material of an object (only once)
	 */
	[RequireComponent (typeof(BoxCollider))]
	public class ChangeMatInteraction: Interactable
	{
		[SerializeField] string actionMessage;
		[SerializeField] Material afterInteract;
		[SerializeField] BoxCollider BoxcolliderObj;

		/**
		 * Interaction text to be shown if the player is in reach
		 */
		override public string interactMessage() {
			return actionMessage;
		}

		/**
		 * Replace the material and destroy the BoxCollider to prevent an additional interaction
		 */
		override public void OnInteractionKeyPressed()
		{
			XplrDebug.LogWriter.Write("Material gewechselt", gameObject);
			gameObject.GetComponent<MeshRenderer>().material = afterInteract;
			BoxcolliderObj.enabled = false;
			this.enabled = false;
		}
	}
}