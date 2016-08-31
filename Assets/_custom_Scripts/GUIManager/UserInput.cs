using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace XplrEvents
{
	/**
	 * Event used to indicate that visible objects have changed.
	 * Use this to redraw special elements like lightbeams
	 */
	public class WordEntered : GameEvent {
		public bool exists;
		public string word;
		public WordEntered (string n, bool e) {
			exists = e;
			word = n;
		}
	}
}

/**
 * Assign mutiple names to one object
 * @see UserInput
 */
[Serializable]
public class NamedObject {
	public GameObject thing;
	public string[] names;
}

namespace ExplorersBook
{
	/**
	 * Handles text input from user. Will be used when the Explorer's Book is open and user wants to enter a word.
	 * Entered words will be set visible in scene. Event listener can be implemented with {@link XplrEvents.WordEntered}
	 * 
	 * @see Room1Manager for an example
	 */
	public class UserInput : MonoBehaviour {
		
		public static bool disableInput = false;
		private bool isUserInputOpen;

		private LinkedList<string> visibleWords = new LinkedList<string>();
		/** This canvas will be used as on/off toggle */
		[SerializeField] private GameObject inputCanvas;
		/** Hook up input field */
		[SerializeField] private InputField wordInputField;
		/** Always display a maximum number of elements, first in first out */
		[SerializeField] private int wordLimit = 3;
		/** The list of objects which can be toggled visible */
		[SerializeField] private NamedObject[] objects;

		/**
		 * Hide all GameObjects and input field
		 */
		void Awake() {
			deactivateAllGameObjects (true);
			setUserInputEnabled (false);
		}
		/**
		* Listen for enter key to start new user input
		*/
		void LateUpdate() {
			if (!disableInput && Input.GetButtonUp ("Spell Word")) {
				if (isUserInputOpen)
					handleUserInput (wordInputField.text);
				setUserInputEnabled (!isUserInputOpen);
			}
		}
		/**
		 * Sets the canvas visible and focus to text field
		 */
		private void setUserInputEnabled (bool flag)
		{
			Reachable.shouldRaycast = !flag;
			MouseCrosshair.showCrosshair = !flag;
			GameManager gameManager = GameManager.getInstance ();
			gameManager.disablePlayerAudioSource (flag);
			gameManager.disableWalking (flag);
			gameManager.disableJumping (flag);

			inputCanvas.SetActive (flag);
			isUserInputOpen = flag;
			if (flag)
				wordInputField.ActivateInputField ();
			else
				wordInputField.DeactivateInputField ();
		}
		/**
		 * Will be called whenever the user enters a new word
		 */
		public void handleUserInput(string inputString) {
			if (inputString == "xplr") {
				deactivateAllGameObjects (false);
				return;
			}
			if (hasObjectsForWord (inputString))
				addWordAndUpdate (inputString);
			else
				Events.instance.Raise (new XplrEvents.WordEntered(inputString, false));
			
			wordInputField.text = "";
			// wordInputField.ActivateInputField ();
		}
		/**
		 * Keeps the visible word list up to date and limited to [wordLimit].
		 * Also deactivates all objects and displays only the selected ones.
		 */
		private void addWordAndUpdate(string word) {
			if (word != null && word.Length > 0) {
				if (visibleWords.Contains (word)) {
					visibleWords.Remove (word);
				} else {
					visibleWords.AddFirst (word);
					while (visibleWords.Count > wordLimit)
						visibleWords.RemoveLast ();
				}
				redisplayCurrentSelection ();
				Events.instance.Raise (new XplrEvents.WordEntered(word, true));
			}
		}
		/**
		 * Will deactivate all objects and display only those which are currently entered
		 */
		private void redisplayCurrentSelection() {
			deactivateAllGameObjects (true);
			foreach (string word in visibleWords)
				foreach (GameObject thing in findObjectsByWord (word))
					setObjectVisible (thing, true);
		}
		/**
		 * Set the visibility of all assigned objects to hidden
		 */
		private void deactivateAllGameObjects(bool flag) {
			foreach (NamedObject obj in objects)
				setObjectVisible (obj.thing, !flag);
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
			Collider[] colliders = go.GetComponentsInChildren<Collider> (true);
			foreach (Collider c in colliders)
				c.enabled = visible;
			Renderer[] renderers = go.GetComponentsInChildren<Renderer> (true);
			foreach (Renderer r in renderers)
				r.enabled = visible;
			Rigidbody[] rigidbodies = go.GetComponentsInChildren<Rigidbody> (true);
			foreach (Rigidbody rb in rigidbodies)
				rb.useGravity = visible;
		}
	}
}