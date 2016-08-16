using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{
		private GameObject explBook;
		private GameObject inputField;
		private CharacterController CharacterController;
		private UnityStandardAssets.Characters.FirstPerson.FirstPersonController FPSControllerScript;
		private MouseCrosshair MouseCrosshair;
		private bool bookIsOpen = false;

		void Start () {
			explBook = GameObject.Find ("Explorers Book");
			inputField = GameObject.Find ("ExplorersWord");
			MouseCrosshair = GameObject.Find ("FirstPersonCharacter").GetComponent<MouseCrosshair> ();
			CharacterController = GameObject.Find ("FPSController").GetComponent<CharacterController> ();
			FPSControllerScript = GameObject.Find ("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
			if (explBook) {
				explBook.SetActive (false);
			}
			if (inputField) {
				inputField.SetActive (false);
			}
		}

		void LateUpdate() {
			if ((Input.GetKeyUp(KeyCode.B) && !bookIsOpen) || (Input.GetKeyUp(KeyCode.Escape) && bookIsOpen) ) {
				openExplorersBook ();
			}
		}

		private void openExplorersBook() { 
			bookIsOpen = !bookIsOpen;
			ActivateExplorersBook ();
			DisablePlayerMovement ();
			UnlockMouseMovement ();
			ActivateUserInputField ();
		}

		private void ActivateExplorersBook() {
			if (explBook) {
				explBook.SetActive (bookIsOpen);
			}
		}

		private void ActivateUserInputField() {
			if (inputField) {
				inputField.SetActive(bookIsOpen);
			}
		}

		private void DisablePlayerMovement() {
			if (FPSControllerScript) {
				FPSControllerScript.enabled = !bookIsOpen;
			}
			if (CharacterController) {
				CharacterController.enabled = !bookIsOpen;
			}
		}

		private void UnlockMouseMovement() {
			Cursor.visible = bookIsOpen;
			Cursor.lockState = CursorLockMode.Confined;
			if (MouseCrosshair) {
				MouseCrosshair.enabled = !bookIsOpen;
			}
		}
			
		void OnGUI() {
		}

	}
}

