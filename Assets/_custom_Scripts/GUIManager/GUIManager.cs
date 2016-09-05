﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProceduralToolkit;

/**
* Handles correct depiction on GUI. 
*/
public class GUIManager : MonoBehaviour
{
	private int padding = 7;
	private Vector2Int boxSize = new Vector2Int(200, 25);
	List<string> messageList  = new List<string> ();
	private string centeredText;

	/**
	* Registers a Text or removes it if it is already registered and should no more be Displayed. 
	*/
	public void register (string textToDisplay, bool shouldBeSet) {
		if (shouldBeSet) {
			if (!messageList.Contains (textToDisplay)) {
				messageList.Add (textToDisplay);
			}
		} else {
			messageList.Remove (textToDisplay);
		}
	}

	/**
	* Displayes the registered Strings and centered strings. 
	*/
	void OnGUI() {
		
		float yWithOffset = Screen.height/2.0f;
		foreach (string reg in messageList) {
			yWithOffset -= boxSize.y + padding;
			GUI.color = Color.white;
			GUI.Box (new Rect (Screen.width/2.0f + padding, yWithOffset, boxSize.x, boxSize.y), reg);
		}
		if (centeredText != null && centeredText != "") {
			GUI.color = Color.white;
			float height = 16.0f * centeredText.Split ('\n').Length + 7.0f;
			GUI.Box(new Rect(Screen.width/2-150, Screen.height/2+20, 300, height), centeredText);
		}
	}

	/**
	* Starts Coroutine for a temporary Message. 
	*/
	public void centeredMessage (string msg, float timeout) {
		StartCoroutine(showTemporaryMessage(msg, timeout));
	}

	/**
	* Sets CenterdText for Temporary Message. 
	*/
	IEnumerator showTemporaryMessage(string msg, float t) {
		centeredText = msg;
		yield return new WaitForSeconds(t);
		centeredText = null;
	}
}