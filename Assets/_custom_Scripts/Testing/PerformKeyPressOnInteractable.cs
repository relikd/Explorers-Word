using UnityEngine;
using System.Collections;
using Interaction;

public class PerformKeyPressOnInteractable : MonoBehaviour {
	void Awake () {
		Interactable[] scripts = GetComponents <Interactable>();
		foreach (Interactable scpt in scripts) {
			scpt.OnInteractionKeyPressed ();
		}
	}
}
