using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;

[Serializable]
public class NamedObject {
	public GameObject thing;
	public string[] names;
}

public class UserInput : MonoBehaviour {

	private LinkedList<string> visibleWords = new LinkedList<string>();

	[SerializeField] private InputField wordInputField;
	[SerializeField] private int wordLimit = 3;
	[SerializeField] private NamedObject[] objects;

	void Awake() {
		deactivateAllGameObjects ();
	}

	/**
	 * Will be called whenever the user enters a new word
	 */
	public void handleUserInput(string inputString) {
		Debug.Log ("the input: "+inputString);
		if (inputString == "all") {
			deactivateAllGameObjects ();
			return;
		}
		bool t = hasObjectsForWord (inputString);
		Debug.Log ("handle input: "+t);
		if (t) {
			addWordAndUpdate (inputString);
			wordInputField.text = "" ;
		}
		wordInputField.ActivateInputField ();
	}
	/**
	 * Keeps the visible word list up to date and limited to [wordLimit].
	 * Also deactivates all objects and displays only the selected ones.
	 */
	private void addWordAndUpdate(string word) {
		if (visibleWords.Contains (word)) {
			visibleWords.Remove (word);
		} else {
			visibleWords.AddFirst (word);
			while (visibleWords.Count > wordLimit)
				visibleWords.RemoveLast ();
		}
		redisplayCurrentSelection ();
	}
	/**
	 * Will deactivate all objects and display only those which are currently entered
	 */
	private void redisplayCurrentSelection() {
		deactivateAllGameObjects ();
		foreach (string word in visibleWords)
			foreach (GameObject thing in findObjectsByWord (word))
				setObjectVisible (thing, true);
	}
	/**
	 * Set the visibility of all assigned objects to hidden
	 */
	private void deactivateAllGameObjects() {
		foreach (NamedObject obj in objects)
			setObjectVisible (obj.thing, false);
	}
	/**
	 * Returns <strong>true</strong> if there are any objects with name
	 */
	private bool hasObjectsForWord(string word) {
		return findObjectsByWord (word).Count > 0;
	}
	/**
	 * Loops through all objects (assigned in the inspector) and find the one with the name
	 */
	private List<GameObject> findObjectsByWord(string word) {
		List<GameObject> allWithThisName = new List<GameObject>();
		foreach (NamedObject obj in objects)
			foreach (string n in obj.names)
				if (string.Equals(n, word, StringComparison.OrdinalIgnoreCase))
					allWithThisName.Add (obj.thing);
		return allWithThisName;
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