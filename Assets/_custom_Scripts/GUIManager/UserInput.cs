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
	public Texture2D icon;
	public string[] names;

//	public override int GetHashCode () { return thing.GetHashCode (); }
//	public override bool Equals (object obj) {
//		if (obj == null) return false;
//		NamedObject other = obj as NamedObject;
//		if (other == null) return false;
//		return other.thing == this.thing;
//	}
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

		private LinkedList<List<NamedObject>> visibleObjects = new LinkedList<List<NamedObject>>();
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
		void Start() {
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
			if (visibleObjects.Count > 0)
				validateIntegrityOfVisibleObjects ();
		}
		/**
		 * Display the Object Icon at the top screen area
		 */
		void OnGUI() {
			const float IconSize = 64;
			const float Padding = 10;
			float total_width = Mathf.Max (wordLimit, visibleObjects.Count) * (IconSize + Padding);
			Rect boxPos = new Rect(Screen.width-total_width, Padding, IconSize, IconSize);

			int count = 0;
			foreach (List<NamedObject> obj in visibleObjects) {
				Texture2D img = obj[0].icon;
				if(img) GUI.Box (boxPos, img);
				else    GUI.Box (boxPos, "no icon");
				boxPos.x += IconSize + Padding;
				count++;
			}
			// draw empty boxes if fewer items than word limit
			while (count < wordLimit) {
				GUI.Box(boxPos, "");
				boxPos.x += IconSize + Padding;
				count++;
			}
		}
		/**
		 * Loop through all visible objects and delete those which dont have children. Will be used for pickable objects like matchbox or screwdriver
		 */
		private void validateIntegrityOfVisibleObjects() {
			foreach (List<NamedObject> obj in visibleObjects)
				if (obj[0].thing.transform.childCount == 0) {
					visibleObjects.Remove (obj);
					// recursively because modifying an array while enumerating is a bad thing
					validateIntegrityOfVisibleObjects();
					break;
				}
		}
		/**
		 * Sets the canvas visible and focus to text field
		 */
		private void setUserInputEnabled (bool flag)
		{
			if (!ExplorersBook.BookController.isBookOpen ()) {
				Reachable.shouldRaycast = !flag;
				MouseCrosshair.showCrosshair = !flag;
			}
			ExplorersBook.BookController.disableBook = flag;
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

			bool validInput = hasObjectsForWord (inputString);
			if (validInput)
				addWordAndUpdate (inputString);

			Events.instance.Raise (new XplrEvents.WordEntered (inputString, validInput));
			LogWriter.WriteLog ("Wort eingegeben: '"+inputString+"' existiert: "+validInput, gameObject);
			wordInputField.text = "";
			// wordInputField.ActivateInputField ();
		}
		/**
		 * Keeps the visible word list up to date and limited to [wordLimit].
		 * Also deactivates all objects and displays only the selected ones.
		 */
		private void addWordAndUpdate(string word) {
			if (word == null || word.Length == 0)
				return;
			word = word.ToUpperInvariant ().Trim ();

			List<NamedObject> foundList = findObjectsByWord (word);
			List<NamedObject> toBeDeleted = findListInVisibleObjects (foundList);
			if (toBeDeleted != null) { // was already in list, so delete it
				visibleObjects.Remove (toBeDeleted);
			} else { // a new word, so add it
				GlobalSoundPlayer.playCorrectWord ();
				visibleObjects.AddFirst (foundList);
				while (visibleObjects.Count > wordLimit)
					visibleObjects.RemoveLast ();
			}
			redisplayCurrentSelection ();
		}
		/**
		 * Return the reference to the found NamedObject List
		 * @return List<NamedObject> of the visibleObjects List. null if not found
		 */
		List<NamedObject> findListInVisibleObjects (List<NamedObject> other) {
			foreach (List<NamedObject> nList in visibleObjects) {
				int nCount = nList.Count;
				if (other.Count == nCount) {
					foreach (NamedObject otherObj in other)
						if (nList.Contains (otherObj))
							nCount--;
					if (nCount == 0)
						return nList;
				}
			}
			return null;
		}
		/**
		 * Will deactivate all objects and display only those which are currently entered
		 */
		private void redisplayCurrentSelection() {
			deactivateAllGameObjects (true);
			foreach (List<NamedObject> nList in visibleObjects)
				foreach (NamedObject nObj in nList)
					setObjectVisible (nObj.thing, true);
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
			if (word == null || word.Length == 0)
				return false;
			return findObjectsByWord (word).Count > 0;
		}
		/**
		 * Loops through all objects (assigned in the inspector) and find the one with the name
		 */
		private List<NamedObject> findObjectsByWord(string word) {
			List<NamedObject> allWithThisName = new List<NamedObject>();
			if (word == null || word.Length == 0)
				return allWithThisName; // return empty list

			word = word.ToUpperInvariant ().Trim ();
			foreach (NamedObject obj in objects)
				if (obj.thing.transform.childCount > 0) // suppress collected items
					foreach (string n in obj.names)
						if (word == n.ToUpperInvariant ())
							allWithThisName.Add (obj);
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
			Light[] lights = go.GetComponentsInChildren <Light>(true);
			foreach (Light l in lights)
				l.enabled = visible;
		}
	}
}