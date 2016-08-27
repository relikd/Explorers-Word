using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

namespace Interaction
{
	/**
	 * Ein Skript, dass es erlaubt, ein Canvas ein- bzw. auszublenden.!!!!!!! Es erfordert, dass dieses Papier ein Child Canvas besitzt. Während der Spieler liest, kann er sich nicht bewegen und die Kameera nicht rotieren.
	 */
	public class ReadInteraction : Interactable
	{
		[SerializeField] 
		string actionMessage;
		[SerializeField]
		string actionMessageWhileOpen;
        [SerializeField]
        Canvas CanvasToShow;
        private CustomFirstPersonController CFPS;
        private MouseCrosshair Mouse;
		private bool reading = false;

		/**
		 * Initialisiert einige Variablen.
		 */
		void Awake() {
			CFPS = GameObject.Find("FPSController").GetComponent<CustomFirstPersonController>();
            Mouse = GameObject.Find("FirstPersonCharacter").GetComponent<MouseCrosshair>();

        }
        /*
         * Gibt je nach Zustand die entsprechende Nachricht zurück.
         */
        override public string interactMessage()
		{
            return (!reading ? actionMessage : actionMessageWhileOpen);
        }

		/**
		 * Schaltet den Lesemodus an bzw. aus, abhängig vom aktuell aktiven Modus. Deaktiviert alle Handlungsmoeglichkeiten des Spielers, bis der Lesemodus beendet wird.
		 */
		override public void OnInteractionKeyPressed()
		{
			if (!reading)
			{
                EnableGUI(false);
				reading = !reading;
				GameManager.getInstance ().disableExplorersBook ();
                CFPS.shouldWalk = false;
                CFPS.shouldJump = false;
                CFPS.shouldLookAround = false;
                CFPS.shouldPlayAudioSounds = false;
                toggleCanvas();
                toggleMouseCrosshair();
            }
			else
			{
                EnableGUI(false);
                reading = !reading;
				GameManager.getInstance ().disableExplorersBook ();
                CFPS.shouldWalk = true;
                CFPS.shouldJump = true;
                CFPS.shouldLookAround = true;
                CFPS.shouldPlayAudioSounds = true;
                toggleCanvas();
                toggleMouseCrosshair();            
            }
		}
		/**
		 * Schaltet die Sichtbarkeit des Canvas um.
		 */
		private void toggleCanvas() 
		{
            CanvasToShow.enabled = reading;

		}
		/**
		 * Scahltet die Sichtbarkeit des Fadenkreuzes um. 
		 */
		private void toggleMouseCrosshair() 
		{
			Mouse.enabled = !reading;

		}
	}
}