using System;
using UnityEngine;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
	{
	class Registered {
		public string textToDisplay;
		bool shuldbeSet;

		public Registered(string text, bool shouldBeSet) {}


	}

	List<string> registeredText  = new List<string> ();
	private float yOffset = 30f;
	Vector2 startingPosition = new Vector2 (Screen.width - Screen.width / 4, Screen.height / 4);


	void OnStart() {
		
	}

	public void register(string textToDisplay, bool shouldBeSet) {
		if (shouldBeSet) {
			
			if (!registeredText.Contains(textToDisplay)) {
				registeredText.Add (textToDisplay);
			}
		} else {
			removeText (textToDisplay);	
		}
	}

	 bool alreadyRegistered(string reg) {
		bool result = false;
		foreach(string regi in registeredText) {
			if (reg == regi) {
				result = true;
			}
		}
		return result;
	}

	 void removeText(string text) {
		foreach(string reg in registeredText) {
			if (reg == text) {
				registeredText.Remove (reg);
			}
		}
	}

	void OnGUI() {
		float newYPossition = startingPosition.y;
		Debug.Log (registeredText.Count);
		foreach (string reg in registeredText) {
			GUI.color = Color.white;
			GUI.Box(new Rect(startingPosition.x, newYPossition, 200, 25), reg);
			newYPossition += yOffset;
		}
	}
}


