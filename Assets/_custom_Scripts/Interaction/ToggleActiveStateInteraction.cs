using UnityEngine;
using System.Collections;

namespace Interaction{

	/*
	 * Ermöglicht den ActiveState von übergebenen GameObjects bei Interaction zwischen aktiv/inaktiv zu schalten
	 */

	public class ToggleActiveStateInteraction : Interactable {

		public GameObject[] ObjectsToToggle;
		public bool ObjectsActiveOnStart;
		private bool areObjectsActive;

		[SerializeField] 
		string actionMessage; 

		void Awake(){
			areObjectsActive = ObjectsActiveOnStart;
			foreach(GameObject obj in ObjectsToToggle){
				obj.SetActive (areObjectsActive);
			}
		}

		/**
		 * Gibt den Text zurueck, der dem Spieler angezeigt wird, wenn er mit der Maus über das interagierbare Objekt faehrt.
		 */ 
		override public string interactMessage() {
			return actionMessage;
		}

		/**
		 * Schaltet Objekte aktiv/inaktiv
		 */
		override public void OnInteractionKeyPressed()
		{
			areObjectsActive = !areObjectsActive;
			foreach(GameObject obj in ObjectsToToggle){
				obj.SetActive (areObjectsActive);
			}

		}
	}
}

