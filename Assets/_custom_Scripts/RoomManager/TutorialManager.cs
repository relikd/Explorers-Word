using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
	public GUITutorialManager GUITutorialManager;
	public string IntroText = "Aus zahlreichen Studien ist bekannt, dass sich ein Mensch maximal 3 Dinge gleichzeitig merken kann. Dies trift auch den Spieler dieser Welt zu, welcher nur 3 Dinge gleichzeitig visualisieren kann. Drücke die Rechte Pfeiltaste um fortzufahren.";
	public string HowToDisplayTheBookText = "Um sich auch eine neue Sache konzentrieren und diese somit zu visualisieren muss der Spieler den Beschriebenen Gegenstand benennen. \n Drücke B um eine kurzbeschreibung deiner Umgebung zu bekommen und ein Eingabefeld anzuzeigen.";
	public string OpenedTheBookText = "Sehr gut du hast erfolgreich das Buch geöffnet. Drücke die Rechte Pfeiltaste um fortzufahren.";
	public string NameAnObjectText = "Nachdem du die Raumbeschreibung auf der rechten Seite gelesen hast kannst du ein Objekt nennen, welches sich wahrscheinlich im Raum befindet. \n Für den Anfang ist dort bereits ein richtiges Wort plaziert, alles was du noch tuhen musst ist, Enter zu drücken.";
	public string PressedEnterText = "Gut gemacht. Um wieder einen Blick auf den Raum zu werfen und sicher zu stellen, dass das genannte Objekt angezeigt wird, drücke Escape.";
	public string VisibleText = "Wie du siehst ist das Objekt nun sichtbar." + " Um das Object erneut unsichtbar zu schalten musst du das Object erneut benennen. Öffne erneut das Buch und benenne das Object und schau dir an ob du das Objekt vergessen hast.";
	public string ObjectInvisibleText = "Wie du siehst hast du das Objekt nun wieder vergessen. Drücke die Rechte Pfeiltaste um fortzufahren.";
	public string ObjectOverflowText = "Nun weißt du wie man Objekte sichtbar visualisiert und vergisst, experimentiere wie man mehrere Objekt auf einmal zu visualisiert oder was passiert wenn man versucht sich zu viel auf einmal vorzustellen. Um das Tutorial zu verlassen laufe zur Tür und interagiere mit ihr.";
	bool tutorialInitilization = false;
	bool shouldDisplayOpenBookText = false;
	bool shouldOpenBook = false;
	bool openedTheBook = false;
	bool shouldNameAnObject = false;
	bool shouldPressEnter = false;
	bool pressedEnter = false;
	bool pressedEscape = false;
	bool shouldNameObjectAgain = false;
	bool shouldExperiment = false;

	/**
	 * 
	 */
	void LateUpdate() {
		if (!tutorialInitilization) {
			GameManager.getInstance ().disableExplorersBook ();
			GameManager.getInstance ().disableRightBookPage ();
			TutorialIntitialization ();
			tutorialInitilization = true;
			GUITutorialManager.register (IntroText, true);
		}
		if (shouldDisplayOpenBookText && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (IntroText, false);
			GUITutorialManager.register (HowToDisplayTheBookText, true);
			lockUserControlls ();
			GameManager.getInstance ().disableExplorersBook ();

			//StartCoroutine( waitAndSetBool(shouldOpenBook, 1.0f));
			shouldDisplayOpenBookText = false;
			shouldOpenBook = true;
		} else if (shouldOpenBook && Input.GetKeyDown (KeyCode.B)) {
			//lockUserControlls ();
			GUITutorialManager.register (HowToDisplayTheBookText, false);
			GUITutorialManager.register (OpenedTheBookText, true);

			shouldOpenBook = false;
			shouldNameAnObject = true;
		} else if (shouldNameAnObject && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (OpenedTheBookText, false);
			GUITutorialManager.register (NameAnObjectText, true);
			GUITutorialManager.WriteToInputField ("books");

			shouldNameAnObject = false;
			shouldPressEnter = true;
		} else if (shouldPressEnter && Input.GetKeyDown (KeyCode.Return)) {
			GUITutorialManager.register (NameAnObjectText, false);
			GUITutorialManager.register (PressedEnterText, true);

			pressedEscape = true;
			shouldPressEnter = false;
		} 
		if (pressedEscape && Input.GetKeyDown (KeyCode.Escape)) {
			GUITutorialManager.register (PressedEnterText, false);
			GUITutorialManager.register (VisibleText, true);

			pressedEscape = false;
			shouldNameObjectAgain = true;
		} else if (shouldNameObjectAgain && Input.GetKeyDown (KeyCode.Escape)) {
			GUITutorialManager.register (VisibleText, false);
			GUITutorialManager.register (ObjectInvisibleText, true);

			shouldNameObjectAgain = false;
			shouldExperiment = true;
		} else if (shouldExperiment && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (ObjectInvisibleText, false);
			GUITutorialManager.register (ObjectOverflowText, true);
			shouldExperiment = false;
		}
	}

	/**
	 * Initialises The Tutorial Scene.
	 */
	private void TutorialIntitialization() {
		lockUserControlls ();
		shouldDisplayOpenBookText = true;
	}

	/**
	 * Locks the User Controlls.
	 */
	private void lockUserControlls() {
		GameManager gameManager = GameManager.getInstance ();
		gameManager.disableJumping ();
		gameManager.disableWalking ();
		gameManager.disablePlayerAudioSource ();
	}

}

