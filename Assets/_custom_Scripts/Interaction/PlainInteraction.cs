using UnityEngine;

namespace Interaction
{
	/**
	 * Display a text on screen
	 */
	public class PlainInteraction : Interactable
	{
		[Tooltip("Interaction [message] to be displayed on screen")]
		public string actionMessage = "interact";
		[Multiline]
		public string responseMessage = "Interaction not possible";

		override public string interactMessage() {
			return actionMessage;
		}
		/** Just display the text on screen */
		override public void OnInteractionKeyPressed() {
			centeredMessage (responseMessage);
		}
	}
}