using UnityEngine;
using Interaction;

namespace XplrDebug
{
	/**
	 * Used for integration tests. Performs a key press event on all attached Interactable scripts
	 * Will use all scripts which are on the same GameObject
	 */
	public class TESTPerformKeyPressOnInteractable : MonoBehaviour
	{
		[SerializeField] private bool onAwake = true;
		[SerializeField] private float afterTimePeriod = 0F;

		private float time = 0.0f;
		private bool alreadyPressed = false;

		/**
		 * If {@link #onAwake} is set, perform keyPress immediately
		 */
		void Awake () {
			if (onAwake)
				performKeyPress ();
		}
		/**
		 * If {@link #afterTimePeriod} is set, perform keyPress after the defined time
		 */
		void Update() {
			time += Time.deltaTime;
			if (!alreadyPressed && time > afterTimePeriod && afterTimePeriod > 0.001F) {
				alreadyPressed = true;
				performKeyPress ();
			}
		}
		/**
		 * Loop through all scripts and perform keyPress on all of them
		 */
		void performKeyPress() {
			Interactable[] scripts = GetComponents <Interactable> ();
			foreach (Interactable scpt in scripts)
				scpt.OnInteractionKeyPressed ();
		}
	}
}