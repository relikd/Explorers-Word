using System.Collections;
using UnityEngine;

namespace Interaction {
	public interface Interactable
	{
		void HandleRaycastCollission ();
		void EnableGUI (bool enable);
		bool shouldDisplayInteraction (); // ask object if it wants to interact with Player
	}

}
