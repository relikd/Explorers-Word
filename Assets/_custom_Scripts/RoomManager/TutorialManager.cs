using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	bool tutorialInitilization = false;
	bool shouldDisplayOpenBookText = true;
	bool shouldOpenBook = false;
	bool shouldNameAnObject = false;
	bool shouldPressEnter = false;
	bool shouldPressEnterAgain = false;
	bool pressedEscape = false;
	bool objectInvisibleAgain = false;
	bool shouldNameObjectAgain = false;

	public Text textBox;
	public InputField inputField; 

	/**
	 * Routine of the Tutorial.
	 */
	void LateUpdate() {

		if (!tutorialInitilization) {
			shouldDisplayOpenBookText = true;
			tutorialInitilization = true;

			textBox.text = "Wie man sieht, sieht man nichts! Der Spieler muss sich " +
				"zunächst auf einen Gegenstand konzentrieren um diesen zu sehen." +
				"\nDrücke 'E' um fortzufahren.";
		}
		else if (shouldDisplayOpenBookText && Input.GetKeyDown (KeyCode.E)) {
			shouldDisplayOpenBookText = false;
			shouldOpenBook = true;

			textBox.text = "Um sich auf eine neue Sache zu konzentrieren und diese " +
				"somit zu visualisieren, muss der Spieler den beschriebenen Gegenstand benennen." +
				"\nDrücke 'B' um das Buch zu öffnen.";
			
		} else if (shouldOpenBook && Input.GetKeyDown (KeyCode.B)) {

			shouldOpenBook = false;
			shouldNameAnObject = true;

			textBox.text = "Sehr gut, du hast erfolgreich das Buch geöffnet. \nDrücke 'E' um fortzufahren.";

		} else if (shouldNameAnObject && Input.GetKeyDown (KeyCode.E)) {
			shouldNameAnObject = false;
			shouldPressEnter = true;

			inputField.text = "Sessel";
			textBox.text = "Nachdem du die Raumbeschreibung auf der rechten Seite gelesen hast, " +
				"kannst du ein Objekt nennen welches sich im Raum befindet. " +
				"\n\nDrücke Enter um das Eingabefeld zu öffnen.";
		} else if(shouldPressEnter && Input.GetKeyDown(KeyCode.Return)){
			shouldPressEnter = false;
			shouldPressEnterAgain = true;

			textBox.text = "Für den Anfang ist dort bereits ein richtiges Wort platziert." +
				"\nDrücke nun Enter um das Objekt anzuzeigen.";
		} else if (shouldPressEnterAgain && Input.GetKeyDown (KeyCode.Return)) {
			pressedEscape = true;
			shouldPressEnterAgain = false;

			textBox.text = "Gut gemacht. Um wieder einen Blick auf den Raum zu werfen und sicher " +
				"zu stellen, dass das genannte Objekt angezeigt wird, drücke wieder 'B'.";
		} 
		if (pressedEscape && Input.GetKeyDown (KeyCode.B)) {
			pressedEscape = false;
			objectInvisibleAgain = true;

			textBox.text = "Wie du siehst ist das Objekt nun sichtbar. " +
			"Um es wieder unsichtbar zu schalten, musst du es erneut eingeben. " +
			"\nÖffne erneut die Texteingabe und schalte das Objekt wieder unsichtbar.";

		} else if (objectInvisibleAgain && Input.GetKeyDown (KeyCode.Return)) {
			shouldNameObjectAgain = true;
			objectInvisibleAgain = false;
		} else if (shouldNameObjectAgain && Input.GetKeyDown (KeyCode.Return)) {
			shouldNameObjectAgain = false;

			textBox.text = "Um das Tutorial zu verlassen laufe zur Tür und drücke 'E' oder die " +
			"linke Maustaste um mit ihr zu interagieren.";

			StartCoroutine (Yielder());
		}
	}

	IEnumerator Yielder(){
		yield return new WaitForSeconds (2.0f);
		GlobalSoundPlayer.playPuzzleSolved ();
	}
}

