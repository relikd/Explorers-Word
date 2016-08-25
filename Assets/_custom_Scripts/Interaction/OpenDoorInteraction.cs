using UnityEngine;

namespace Interaction
{
	/**
	 * Replace {@link PlainInteraction} with this open door script
	 */
	public class OpenDoorInteraction : Interactable
	{
		/**
		 * Removes the assigned {@link PlainInteraction} on initialize
		*/
		void Awake() {
			PlainInteraction old = GetComponent<PlainInteraction> ();
			if (old)
				Destroy (old);
		}

		public override string interactMessage () {
			return "Open Door With Key";
		}

		public override void OnInteractionKeyPressed () {
			LevelManager.LoadNextRoom ();
		}
	}
}