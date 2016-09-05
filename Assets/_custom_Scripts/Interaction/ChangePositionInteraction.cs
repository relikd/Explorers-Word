using UnityEngine;
using System.Collections;
using Interaction;

namespace Interaction{

	/**
	 * Erlaubt die Position eines Objektes, zeitgesteuert an eine bestimmte Endpostition zu translieren. 
	 */
	public class ChangePositionInteraction : Interactable {

		[SerializeField] private bool _destroyWhenFinished;
		[SerializeField] private Vector3 _endposition;
		[SerializeField] [Range(0.1f,5.0f)] private float _translationSpeed;
		private Transform _currentObjectPosition;

		[SerializeField] 
		string actionMessage; 

		private bool isMoving = false;

		void Awake(){
			_currentObjectPosition = gameObject.GetComponent<Transform> ();
		}

		void Update(){
			if (isMoving) {
				if (_endposition == _currentObjectPosition.position) {
					isMoving = false;
					if (_destroyWhenFinished) {
						DestroyObject (this.gameObject);
					} else {
						interactionEnabled = true;
					}
				} else {
					_currentObjectPosition.position =  Vector3.MoveTowards (_currentObjectPosition.position, _endposition, 0.001f * _translationSpeed);
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
			isMoving = true;
			interactionEnabled = false;
		}
	}
}

