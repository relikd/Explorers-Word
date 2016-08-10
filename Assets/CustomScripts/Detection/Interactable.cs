using System.Collections;
using UnityEngine;

namespace Interaction {
	public interface Interactable
	{
		void HandleRaycastCollission();
		void EnableGUI(bool enable);
	}

}
