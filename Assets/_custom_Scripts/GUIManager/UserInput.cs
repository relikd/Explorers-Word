using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class NamedObject {
	public GameObject thing;
	public string[] names;
}

public class UserInput : MonoBehaviour {

	private int QueueLength = 3;
	private LinkedList<GameObject> visibleObjects = new LinkedList<GameObject>();

	public InputField wordInputField;
	public NamedObject[] objects;

	public void handleUserInput(string inputString) {
		if (inputString == "all") {
			deactivateAllGameObjects ();
		}

		GameObject go = findObjectByWord (inputString);
		if (visibleObjects.Contains (go)) {
			visibleObjects.Remove (go);
			setObjectVisible (go, false);
		} else {
			visibleObjects.AddFirst (go);
			setObjectVisible (go, true);
			while (visibleObjects.Count > QueueLength) {
				visibleObjects.RemoveLast ();
			}
		}

//		if (!handleGameObjectInQueue (UserInput)) {
//			disableGameObjectViaTag (UserInput);
//		}

		wordInputField.ActivateInputField ();
		wordInputField.text = "" ;
	}

	/**
	 * Loops through all objects (assigned in the inspector) and find the one with the name
	 */
	private GameObject findObjectByWord(string word) {
		foreach (NamedObject obj in objects)
			foreach (string n in obj.names)
				if (string.Equals(n, word, StringComparison.OrdinalIgnoreCase))
					return obj.thing;
		return null;
	}
	/**
	 * Set the visibility of all assigned objects to hidden
	 */
	public void deactivateAllGameObjects() {
		foreach (NamedObject obj in objects)
			setObjectVisible (obj.thing, false);
	}

	/**
	 * For a single Object (and all children), deactivate the Collider, Renderer and Rigidbody
	 */
	private void setObjectVisible(GameObject go, bool visible) {
		Collider[] colliders = go.GetComponentsInChildren<Collider> ();
		foreach (Collider c in colliders) {
			c.enabled = visible;
		}
		Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = visible;
		}
		// TODO: set all children too
		Rigidbody rigidbody = go.GetComponent<Rigidbody> ();
		if (rigidbody) {
			rigidbody.useGravity = visible;
		}
		go.transform.hasChanged = true;
	}
}