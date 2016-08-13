using System.Collections;
using UnityEngine;
using UnityEditor.VersionControl;

namespace Interaction {
	public abstract class Interactable : MonoBehaviour
	{
		[Tooltip("The KeyCode needed to trigger the action")]
		public char interactKey = 'E';
//		[Tooltip("Press 'E' to [message]")]
//		public string interactMessage = "rotate";

		abstract public void HandleRaycastCollission ();
		abstract public string interactMessage();

		// use this function to get the current KeyCode mapping
		protected KeyCode theKeyCode() {
			return (KeyCode) System.Enum.Parse(typeof(KeyCode), interactKey.ToString ().ToUpper ());
		}

		// ask object if it wants to interact with Player
		virtual public bool shouldDisplayInteraction () {
			return true; // override this in your custom class
		}

		// used to display text on screen
		public void EnableGUI (bool enable) {
			GUIManager gm = GameObject.FindObjectOfType<GUIManager> ();
			if (gm) gm.register ("Press '"+interactKey.ToString ().ToUpper ()+"' to "+interactMessage(), enable);
		}
	}
}
