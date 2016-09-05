using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XplrGUI {
	/**
	* Handles the display of all screen messages like Interaction text and centered response text
	*/
	public class ScreenMessage : MonoBehaviour
	{
		static private ScreenMessage _i;

		private const int padding = 7;
		private static Vector2 boxSize = new Vector2(200, 25);
		private static List<string> messageList = new List<string> ();
		private static string centeredText;

		void Awake() {
			if (_i == null)
				_i = this;
		}

		/**
		* Registers an action message or removes it if it was already registered
		*/
		public static void registerActionMessage (string textToDisplay, bool shouldBeSet) {
			if (shouldBeSet) {
				if (!messageList.Contains (textToDisplay)) {
					messageList.Add (textToDisplay);
				}
			} else {
				messageList.Remove (textToDisplay);
			}
		}
		/**
		* Display all action messages and centered text
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
		* Display a text in the middle of the screen
		* @param msg text to be displayed
		* @param timeout time in seconds
		*/
		public static void centeredMessage (string msg, float timeout) {
			ScreenMessage._i.StartCoroutine(ScreenMessage._i.showTemporaryMessage(msg, timeout));
		}
		/**
		* Coroutine to reset text after a few seconds
		*/
		IEnumerator showTemporaryMessage(string msg, float t) {
			centeredText = msg;
			yield return new WaitForSeconds(t);
			centeredText = null;
		}
	}
}