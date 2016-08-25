using System.Collections;
using UnityEngine;
using System;

namespace Interaction {
	[RequireComponent (typeof(Collider))]
	public abstract class Interactable : MonoBehaviour
	{
		public bool interactionEnabled = true;
		[Tooltip("The KeyCode needed to trigger the action")]
		public char interactionKey = 'E';
		[SerializeField] private AudioClip[] m_Sounds; 
		private AudioSource m_AudioSource;

		abstract public string interactMessage ();
		abstract public void OnInteractionKeyPressed ();
		virtual public void OnInteractionKeyHold () {} // @overwrite
        virtual public void OnInteractionKeyDown () {} // @overwrite
		//
		// Raycasting methods
		//

		// ask object if it wants to interact with Player
		virtual public bool shouldDisplayInteraction () {
			return interactionEnabled; // override this in your custom class
		}

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

            //			if (Input.GetKeyUp (theKeyCode ())) {
            //				playInteractionSound ();
            //				OnInteractionKeyPressed ();
            //			}
            //			if (Input.GetKey (theKeyCode ()))
            //				OnInteractionKeyHold ();
        }

		// used to display text on screen
		public void EnableGUI (bool enable) {
			string msg = interactMessage ();
			if (msg != null && msg != "") {
				GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
				if (gm) gm.register ("Press '"+interactionKey.ToString ().ToUpper ()+"' to "+msg, enable);
			}
		}

		//
		// Methods
		//

		// use this function to get the current KeyCode mapping
		protected KeyCode theKeyCode() {
			return (KeyCode) System.Enum.Parse(typeof(KeyCode), interactionKey.ToString ().ToUpper ());
		}

		// Display message in the middle of the screen
		protected void centeredMessage(string message) {
			centeredMessage (message, 3);
		}

		protected void centeredMessage(string message, float timeout) {
			GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
			if (gm) gm.centeredMessage (message, timeout);
		}

		private void playInteractionSound(){
			try{
				m_AudioSource = gameObject.GetComponent<AudioSource>();
				if (m_AudioSource) {
					if(m_Sounds.Length != 1){
						int n = UnityEngine.Random.Range(1, m_Sounds.Length);			
						m_AudioSource.clip = m_Sounds[n];
						m_AudioSource.Play();
						// move picked sound to index 0 so it's not picked next time
						m_Sounds[n] = m_Sounds[0];
						m_Sounds[0] = m_AudioSource.clip;
					}
					else{
						m_AudioSource.clip = m_Sounds[0];
						m_AudioSource.Play();
					}
				}
			}
			catch(Exception e) {
				Debug.LogException (e);
			}
		}
	}
}
