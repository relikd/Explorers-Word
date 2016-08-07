using UnityEngine;
using System.Collections;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{

		public GameObject explBook;
		private bool isActive = false;
		void LateUpdate() {

			if (Input.GetKeyUp(KeyCode.B)) {
				//GameObject expBook = GameObject.Find ("ExplorersBook");
				isActive = !isActive;
				explBook.SetActive (isActive);
				DisablePlayerMovement ();
				UnlockMouseMovement ();
			}
		}

		private void DisablePlayerMovement() {
			GameObject player = GameObject.Find ("FPSController");
			player.GetComponent<CharacterController>().enabled = !isActive;
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = !isActive;
		}

		private void UnlockMouseMovement() {
			GameObject FirstPersonCharacter = GameObject.Find ("FirstPersonCharacter");
			MouseLock mouseLock = FirstPersonCharacter.GetComponent<MouseLock>();
			mouseLock.enabled = !isActive;
			Cursor.visible = isActive;
			Cursor.lockState = CursorLockMode.Confined;
			FirstPersonCharacter.GetComponent<MouseCrosshair> ().enabled = !isActive;
		}

	}
}

