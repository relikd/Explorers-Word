using UnityEngine;

namespace Interaction
{
	/**
	 * Display a text on screen
	 */
	public class PlainInteraction : Interactable
	{
		[Tooltip("Interaction [message] to be displayed on screen")]
		/** Interaction text to be displayed at the right side of the screen */
		public string actionMessage = "interact";
		/** Response text to be displayed in the center of the screen after intercation happend */
		[Multiline] public string responseMessage = "Interaction not possible";

		override public string interactMessage() {
			XplrDebug.LogWriter.Write ("Action Message: " + actionMessage, gameObject);
			return actionMessage;
		}
		/** Just display the text on screen */
		override public void OnInteractionKeyPressed() {
			XplrDebug.LogWriter.Write ("Centered Message: " + responseMessage, gameObject);
			centeredMessage (responseMessage);
		}
	}
}