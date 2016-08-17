using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{
		private GameObject explBook;
		private GameObject inputField;
		private CharacterController CharacterController;
		private MouseCrosshair MouseCrosshair;
		private UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSControllerScript;
		//private StoryManager StoryManager;
		//GameObject RoomDescription; 
		private bool bookIsOpen = false;
		private float lastWalkingSpeed;
		private float lastRunningSpeed;

		void Start () {
			explBook = GameObject.Find ("Explorers Book");
			inputField = GameObject.Find ("ExplorersWord");
			MouseCrosshair = GameObject.Find ("FirstPersonCharacter").GetComponent<MouseCrosshair> ();
			CharacterController = gameObject.GetComponentInParent<CharacterController> ();
			FPSControllerScript = GameObject.Find ("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
			//StoryManager = gameObject.GetComponent<StoryManager> ();
			//RoomDescription = GameObject.Find("RoomDescription");

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
			//ActivateStoryManager ();

		}

		private void ActivateStoryManager() {
			//StoryManager.enabled = bookIsOpen;
			//RoomDescription.SetActive (bookIsOpen);
		}

		private void ActivateExplorersBook() {
			if (explBook && bookIsOpen) {
				explBook.SetActive (bookIsOpen);
				StartCoroutine (playAnimation ());
			} else {
				StartCoroutine(playAnimation ());
			}
		}

		private void ActivateUserInputField() {
			if (inputField) {
				inputField.SetActive(bookIsOpen);
				InputField inputFieldComponent = inputField.GetComponent<InputField> ();
				inputFieldComponent.ActivateInputField ();
			}
		}

		private void DisablePlayerMovement() {
			if (CharacterController) {
				CharacterController.enabled = !bookIsOpen;
			}

			if (FPSControllerScript) {
				lastRunningSpeed = FPSControllerScript.m_RunSpeed;
				lastWalkingSpeed = FPSControllerScript.m_WalkSpeed;

				FPSControllerScript.m_RunSpeed = 0;
				FPSControllerScript.m_WalkSpeed = 0;
			}
		}

		private void UnlockMouseMovement() {
			Cursor.visible = bookIsOpen;
			Cursor.lockState = CursorLockMode.Confined;
			if (MouseCrosshair) {
				MouseCrosshair.enabled = !bookIsOpen;
			}
		}
			
		IEnumerator playAnimation() {
			Animator anim = explBook.GetComponent<Animator> ();
			if (anim) {
				anim.SetBool ("open", bookIsOpen);
				yield return new WaitForSeconds (getAnimationDuration (anim, "open"));
				explBook.SetActive (bookIsOpen); //only matters when book is Open == false. 
			}
		}

		private float getAnimationDuration(Animator a, string n) {
			foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
				if (clip.name == n) return clip.length;
			return 0;
		}
			
		void OnGUI() {
		}

	}
}

