using UnityEngine;
using Interaction;

namespace XplrDebug {
	public class TESTPerformKeyPressOnInteractable : MonoBehaviour {
		void Awake () {
			Interactable[] scripts = GetComponents <Interactable>();
			foreach (Interactable scpt in scripts)
				scpt.OnInteractionKeyPressed ();
		}
	}
}