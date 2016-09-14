using UnityEngine;
using System.Collections;
using Interaction;

namespace Interaction{

	/**
	 * Erlaubt die Position eines Objektes, zeitgesteuert an eine bestimmte Endpostition zu translieren. 
	 */
	public class ChangePositionInteraction : Interactable {

		[SerializeField] string actionMessage;
		[SerializeField] private bool _destroyWhenFinished;
		[SerializeField] private Vector3 _endposition;
		[SerializeField] private float translationDuration = 1.0f;

		private Vector3 startingPosition;
		private bool isMoving = false;
		private float moveProgress = 0.0f;
		private Vector3 movingDirection;

		/** Moves the object to the new position and deletes it if selected */
		void Update(){
			if (isMoving) {
				moveProgress += (Time.deltaTime / translationDuration);
				moveProgress = Mathf.Min (moveProgress, 1.0f);
				gameObject.transform.position =  startingPosition + (movingDirection * moveProgress);
				if (moveProgress >= 1.0f) {
					isMoving = false;
					moveProgress = 0.0f;
					if (_destroyWhenFinished) {
						DestroyObject (this.gameObject);
					}
				}
			}
		}

		/**
		 * Gibt den Text zurueck, der dem Spieler angezeigt wird, wenn er mit der Maus über das interagierbare Objekt faehrt.
		 */ 
		override public string interactMessage() {
			return actionMessage;
		}

		/**
		 * Startet die Translation
		 */
		override public void OnInteractionKeyPressed()
		{
			XplrDebug.LogWriter.Write("Position durch Script geändert", gameObject);
			startingPosition = gameObject.transform.position;
			movingDirection = _endposition - startingPosition;
			isMoving = true;
			interactionEnabled = false;
		}
	}
}

