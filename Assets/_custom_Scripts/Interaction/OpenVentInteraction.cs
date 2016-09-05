using UnityEngine;

namespace Interaction
{
	/**
	* Functionality for the Vent Interaction in Room 3.
	*/
	public class OpenVentInteraction : PlainInteraction {

		[SerializeField] private GameObject[] Screws;
		[Multiline]
		[SerializeField] private string messageIfAllUnscrewed;

		/**
		 * Gives the response Message.
		 */
		public override string interactMessage () {
			return actionMessage;
		}
		/**
		 * Method for checking if the Screws are removed.
		 */
		private bool checkIfAllScrewsAreRemoved() {
			int count = 0;
			foreach (GameObject screw in Screws) {
				if (screw == null) {
					count++;
				}
			}
			return (count == Screws.Length);
		}
		/**
		 * If all screws are removed and an interaction takes place, it loads the next Room .
		 */
		public override void OnInteractionKeyPressed () {
			if (checkIfAllScrewsAreRemoved ()) {
				responseMessage = messageIfAllUnscrewed;
				gameObject.SetActive (false);
				XplrDebug.LogWriter.Write ("Opened Vent", gameObject);
			} else {
				XplrDebug.LogWriter.Write ("Tryed to Open Vent", gameObject);
			}
			base.OnInteractionKeyPressed ();
		}
	}
}