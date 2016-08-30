using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
	public GUITutorialManager GUITutorialManager;
	private string IntroTexte = "Aus zahlreichen Studien ist bekannt, dass sich ein Mensch maximal 3 Dinge gleichzeitig merken kann. Dies trifft auch auf den Spieler dieser Welt zu, welcher nur 3 Dinge gleichzeitig visualisieren kann. \nDrücke die Rechte Pfeiltaste um fortzufahren.";
	private string HowToDisplayTheBookText = "Um sich auf eine neue Sache zu konzentrieren und diese somit zu visualisieren, muss der Spieler den Beschriebenen Gegenstand benennen. \nDrücke 'B' um eine Kurzbeschreibung deiner Umgebung zu bekommen und ein Eingabefeld anzuzeigen.";
	private string OpenTheBookText = "Sehr gut du hast erfolgreich das Buch geöffnet. \nDrücke die Rechte Pfeiltaste um fortzufahren.";
	private string NameAnObjectText = "Nachdem du die Raumbeschreibung auf der rechten Seite gelesen hast, kannst du ein Objekt nennen welches sich wahrscheinlich im Raum befindet. \nFür den Anfang ist dort bereits ein richtiges Wort platziert, alles was du noch machen musst ist Enter zu drücken.";
	private string PressedEnterText = "Gut gemacht. Um wieder einen Blick auf den Raum zu werfen und sicher zu stellen, dass das genannte Objekt angezeigt wird, drücke die ^ Taste.";
	private string VisibleText = "Wie du siehst ist das Objekt nun sichtbar. \nUm das Objekt erneut unsichtbar zu schalten musst du das Objekt erneut benennen. Öffne erneut das Buch und benenne das Objekt und schau dir an ob du das Objekt vergessen hast.";
	private string InvisibleText = "Wie du siehst hast du das Objekt nun wieder vergessen. \nDrücke die Rechte Pfeiltaste um fortzufahren.";
	private string OverflowText = "Nun weißt du wie man Objekte visualisiert und vergisst. Experimentiere wie man mehrere Objekt gleichzeitig visualisiert. \nUm das Tutorial zu verlassen laufe zur Tür und drücke 'E' oder die linke Maustaste um mit ihr zu interagieren.";
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
	 * Routine of the Tutorial.
	 */
	void LateUpdate() {
		if (!tutorialInitilization) {
			GameManager.getInstance ().disableExplorersBook ();
			GameManager.getInstance ().disableRightBookPage ();
			TutorialIntitialization ();
			tutorialInitilization = true;
			GUITutorialManager.register (IntroTexte, true);
		}
		if (shouldDisplayOpenBookText && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (IntroTexte, false);
			GUITutorialManager.register (HowToDisplayTheBookText, true);
			lockUserControlls ();
			GameManager.getInstance ().disableExplorersBook ();

			shouldDisplayOpenBookText = false;
			shouldOpenBook = true;
		} else if (shouldOpenBook && Input.GetKeyDown (KeyCode.B)) {
			GUITutorialManager.register (HowToDisplayTheBookText, false);
			GUITutorialManager.register (OpenTheBookText, true);

			shouldOpenBook = false;
			shouldNameAnObject = true;
		} else if (shouldNameAnObject && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (OpenTheBookText, false);
			GUITutorialManager.register (NameAnObjectText, true);
			GUITutorialManager.WriteToInputField ("Bücher");

			shouldNameAnObject = false;
			shouldPressEnter = true;
		} else if (shouldPressEnter && Input.GetKeyDown (KeyCode.Return)) {
			GUITutorialManager.register (NameAnObjectText, false);
			GUITutorialManager.register (PressedEnterText, true);

			pressedEscape = true;
			shouldPressEnter = false;
		} 
		if (pressedEscape && Input.GetKeyDown (KeyCode.Caret)) {
			GUITutorialManager.register (PressedEnterText, false);
			GUITutorialManager.register (VisibleText, true);

			pressedEscape = false;
			shouldNameObjectAgain = true;
		} else if (shouldNameObjectAgain && Input.GetKeyDown (KeyCode.Caret)) {
			GUITutorialManager.register (VisibleText, false);
			GUITutorialManager.register (InvisibleText, true);

			shouldNameObjectAgain = false;
			shouldExperiment = true;
		} else if (shouldExperiment && Input.GetKeyDown (KeyCode.RightArrow)) {
			GUITutorialManager.register (InvisibleText, false);
			GUITutorialManager.register (OverflowText, true);
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

