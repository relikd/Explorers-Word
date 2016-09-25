using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RoomManager
{
	/** Go through a step by step tutorial guide */
	public class TutorialManager : MonoBehaviour
	{
		private int tutorialStep = 0;
		public Text textBox;
		public InputField inputField;
		public GameObject sessel;

		/**
		 * Step by step guide through tutorial
		 */
		void LateUpdate() {
			if (tutorialStep == 0) {
				tutorialStep++;
				textBox.text = "Wie du siehst, ist ist der Raum leer! Du musst dich " +
					"zunächst auf einen Gegenstand konzentrieren um diesen sehen zu können." +
					"\n\nClick mit der Maustaste um fortzufahren.";
			}
			else if (tutorialStep == 1 && Input.GetKeyDown (KeyCode.Mouse0)) {
				tutorialStep++;
				textBox.text = "Öffne das Buch, um zu sehen welche Objekte es hier gibt." +
					"\n\nDrücke 'B' um das Buch zu öffnen.";
			}
			else if (tutorialStep == 2 && Input.GetKeyUp (KeyCode.B)) {
				tutorialStep++;
				textBox.text = "Sehr gut, nun weißt du welche Objekte im Raum sind und kannst diese einblenden." +
					"\n\nDrücke Enter um das Eingabefeld zu öffnen.";
			}
			else if(tutorialStep == 3 && Input.GetKeyUp(KeyCode.Return)){
				tutorialStep++;
				inputField.text = "Sessel";
				textBox.text = "Für den Anfang ist dort bereits ein richtiges Wort platziert." +
					"\n\nDrücke nun Enter um das Objekt anzuzeigen.";
			}
			else if (tutorialStep == 4 && Input.GetKeyUp (KeyCode.Return)) {
				tutorialStep++;
				textBox.text = "Gut gemacht. Du kannst das Buch nun wieder wegstecken und einen Blick auf den Raum werfen." +
					"\n\nDrücke wieder 'B'";
			}
			else if (tutorialStep == 5 && Input.GetKeyUp (KeyCode.B)) {
				tutorialStep++;
				textBox.text = "Wie du siehst ist das Objekt nun sichtbar. " +
				"Um es wieder unsichtbar zu schalten, musst du es erneut eingeben." +
				"\n\nÖffne erneut die Texteingabe und schalte das Objekt wieder unsichtbar.";
			}
			else if (tutorialStep == 6 && Input.GetKeyUp (KeyCode.Return)) {
				if (sessel.GetComponent<Renderer> ().enabled == false) {
					tutorialStep++;
					textBox.text = "Um das Tutorial zu verlassen, laufe zur Tür und drücke 'E' oder die " +
						"linke Maustaste um mit ihr zu interagieren.";
					StartCoroutine (Yielder());
				}
			}
		}
		/** Play puzzle solved sound */
		IEnumerator Yielder(){
			yield return new WaitForSeconds (2.0f);
			GlobalSoundPlayer.playPuzzleSolved ();
		}
	}
}