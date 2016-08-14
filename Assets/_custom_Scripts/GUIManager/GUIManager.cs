using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
	private float yOffset = 30f;
	Vector2 startingPosition = new Vector2 (Screen.width - Screen.width / 4, Screen.height / 4);
	List<string> messageList  = new List<string> ();
	private string centeredText;

	public void register (string textToDisplay, bool shouldBeSet) {
		if (shouldBeSet) {
			if (!messageList.Contains (textToDisplay)) {
				messageList.Add (textToDisplay);
			}
		} else {
			messageList.Remove (textToDisplay);
		}
	}

	bool alreadyRegistered (string reg) {
		bool result = false;
		foreach(string regi in messageList) {
			if (reg == regi) {
				result = true;
			}
		}
		return result;
	}

	void OnGUI() {
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

	public void centeredMessage (string msg) {
		StartCoroutine(showTemporaryMessage(msg));
	}

	IEnumerator showTemporaryMessage(string msg) {
		centeredText = msg;
		yield return new WaitForSeconds(3);
		centeredText = null;
	}
}