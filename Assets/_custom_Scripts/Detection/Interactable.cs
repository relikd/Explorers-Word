using UnityEngine;

namespace Interaction
{
	/**
	 * Generic Interaction class for handling all interactions.
	 * All interactions should subclass this class to get the same experience across all scripts
	 */
	[RequireComponent (typeof(Collider))]
	public abstract class Interactable : MonoBehaviour
	{
		/** Turn off interaction */
		public bool interactionEnabled = true;
		[SerializeField] private AudioClip[] m_Sounds; 

		/** String to be shown as interact text */
		abstract public string interactMessage ();
		/** Will be called on interaction KeyUP() */
		abstract public void OnInteractionKeyPressed ();
		/** Will be called during interaction key UP and DOWN */
		virtual public void OnInteractionKeyHold () {} // @overwrite
		/** Will be called on interaction KeyDOWN() */
		virtual public void OnInteractionKeyDown () {} // @overwrite

		//
		// Raycasting methods
		//

		/**
		 * Ask object if it wants to interact with Player
		 */
		virtual public bool shouldDisplayInteraction () {
			return interactionEnabled; // override this in your custom class
		}
		/**
		 * Will be called from {@link Reachable} when object is in reach
		 * @see Reachable
		 */
		public void HandleRaycastCollission () {
			if (Input.GetButtonUp ("Interact")) {
				playInteractionSound ();
				OnInteractionKeyPressed ();
			}
			if (Input.GetButton ("Interact")) {
				OnInteractionKeyHold ();
			}
			if (Input.GetButtonDown("Interact"))
			{
				OnInteractionKeyDown();
			}
		}
		/**
		 * Will inform {@link GUIManager} to display text on the right side of the screen
		 * @param enable add or remove text
		 * @see GUIManager
		 */
		public void EnableGUI (bool enable) {
			string msg = interactMessage ();
			if (msg != null && msg != "") {
				GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
				if (gm) gm.register (msg, enable);
			}
		}

		//
		// Methods
		//

		/**
		 * Display a message in the middle of the screen with a 3 sec timeout
		 * @param message text to be displayed
		 */
		protected void centeredMessage(string message) {
			centeredMessage (message, 3);
		}
		/**
		 * Display a message in the middle of the screen
		 * @param message text to be displayed
		 * @param timeout duration visible on screen (in seconds)
		 */
		protected void centeredMessage(string message, float timeout) {
			GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
			if (gm) gm.centeredMessage (message, timeout);
		}
		/**
		 * Play any assigned sound for each {@link #OnInteractionKeyPressed()}
		 * @see #OnInteractionKeyPressed()
		 */
		protected void playInteractionSound(){
			if (m_Sounds != null) {
				if (m_Sounds.Length == 1) {
					AudioSource.PlayClipAtPoint (m_Sounds [0], gameObject.transform.position);
				} else if (m_Sounds.Length > 2) {
					int n = UnityEngine.Random.Range(1, m_Sounds.Length);
					AudioSource.PlayClipAtPoint (m_Sounds [n], gameObject.transform.position);
					// move picked sound to index 0 so it's not picked next time
					AudioClip currentClip = m_Sounds[n];
					m_Sounds[n] = m_Sounds[0];
					m_Sounds [0] = currentClip;
				}
			}
		}

		protected void playInteractionSound(int index){
			if (m_Sounds.Length >= index+1) {
				AudioSource.PlayClipAtPoint ( m_Sounds [index],this.gameObject.transform.position);
			}
		}
	}
}
