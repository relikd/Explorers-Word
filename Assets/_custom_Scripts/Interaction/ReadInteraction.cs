using UnityEngine;

namespace Interaction
{
	/**
	 * Ein Skript, dass es erlaubt, ein Canvas ein- bzw. auszublenden. Beim Anzeigen des Canvas wird die Steuerung des Spielers deaktiviert.
	 */
	public class ReadInteraction : Interactable
	{
		[SerializeField] string actionMessage;
		[SerializeField] string actionMessageWhileOpen;
		[SerializeField] Canvas CanvasToShow;
		private bool reading = false;

		/*
		 * Gibt je nach Zustand die entsprechende Nachricht zurück.
		 */
		override public string interactMessage() {
			return (!reading ? actionMessage : actionMessageWhileOpen);
		}
		/**
		 * Schaltet den Lesemodus an bzw. aus, abhängig vom aktuell aktiven Modus.
		 */
		override public void OnInteractionKeyPressed() {
			setCanvasVisible (!reading);
			LogWriter.WriteLog ("Set Canvas to: " + (!reading).ToString(), gameObject);
		}
		/**
		 * Schaltet die Sichtbarkeit des Canvas um.
		 * Deaktiviert alle Handlungsmoeglichkeiten des Spielers, bis der Lesemodus beendet wird.
		 */
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