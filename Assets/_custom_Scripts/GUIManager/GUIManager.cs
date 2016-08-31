using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
* Handles correct depiction on GUI. 
*/
public class GUIManager : MonoBehaviour
{
	private float yOffset = 30f;
	Vector2 startingPosition;
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
		startingPosition = new Vector2 (Screen.width - Screen.width / 4, Screen.height / 4);
		float newYPossition = startingPosition.y;
		foreach (string reg in messageList) {
			GUI.color = Color.white;
			GUI.Box (new Rect (startingPosition.x, newYPossition, 200, 25), reg);
			newYPossition += yOffset;
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