using UnityEngine;

namespace Interaction
{
	/** Enables a canvas and locks player movement */
	public class ReadInteraction : Interactable
	{
		[SerializeField] string actionMessage;
		[SerializeField] string actionMessageWhileOpen;
		[SerializeField] Canvas CanvasToShow;
		private bool reading = false;

		/* Show interaction message depending on current state */
		override public string interactMessage() {
			return (!reading ? actionMessage : actionMessageWhileOpen);
		}
		/** Toggles the current reading state */
		override public void OnInteractionKeyPressed() {
			setCanvasVisible (!reading);
			XplrDebug.LogWriter.Write ("Set Canvas to: " + (!reading).ToString(), gameObject);
		}
		/** Toggles the canvas and disables player movement until finished */
		private void setCanvasVisible (bool flag) {
			EnableGUI(false);

			reading = flag;
			XplrGUI.MouseCrosshair.showCrosshair = !flag;
			XplrGUI.BookController.disableBook = flag;
			XplrGUI.UserInput.disableInput = flag;

			GameManager gameManager = GameManager.getInstance ();
			gameManager.disablePlayerAudioSource (flag);
			gameManager.disableWalking (flag);
			gameManager.disableJumping (flag);
			gameManager.disableCammeraRotation (flag);

			CanvasToShow.enabled = flag;
		}
	}
}