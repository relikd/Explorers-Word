using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{
		private GameObject explBook;
		private GameObject inputField;
		private CharacterController CharacterController;
		private MouseCrosshair MouseCrosshair;
		private UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSControllerScript;
		private AudioSource PlayerSound;
		private GameObject StoryTextLeft;
		private GameObject StoryTextRight;
		private LevelManager lvlManager;
		private bool bookIsOpen = false;
		private float lastWalkingSpeed;
		private float lastRunningSpeed;
		private List<string> ExplorersStory = new List<string>();
		private int currentStoryIndex = 0;
	

		void Awake() {
			explBook = GameObject.Find ("Explorers Book");
			inputField = GameObject.Find ("ExplorersWord");
			MouseCrosshair = GameObject.Find ("FirstPersonCharacter").GetComponent<MouseCrosshair> ();
			CharacterController = gameObject.GetComponentInParent<CharacterController> ();
			FPSControllerScript = GameObject.Find ("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
			PlayerSound = gameObject.GetComponentInParent<AudioSource> ();
			StoryTextLeft = GameObject.Find ("StoryLeft");
			StoryTextRight = GameObject.Find ("StoryRight");
			lvlManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		
			if (explBook) {
				explBook.SetActive (false);
			}
			if (inputField) {
				inputField.SetActive (false);
			}
		}

		void Start () {
			
		}

		void LateUpdate() {
			if ((Input.GetKeyUp(KeyCode.B) && !bookIsOpen) || (Input.GetKeyUp(KeyCode.Escape) && bookIsOpen) ) {
				openExplorersBook ();
			}
		}

		public void openExplorersBook() { 
			bookIsOpen = !bookIsOpen;
			ActivateExplorersBook ();
			DisablePlayerSound ();
			DisablePlayerMovement ();
			UnlockMouseMovement ();
			ActivateUserInputField ();
			depictExplorersStory ();
		}

		private void ActivateExplorersBook() {
			if (explBook && bookIsOpen) {
				explBook.SetActive (bookIsOpen);
				StartCoroutine (playAnimation ());
			} else {
//				StoryTextLeft.SetActive (bookIsOpen);
//				StoryTextRight.SetActive (bookIsOpen);
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

		private void DisablePlayerSound() {
			if (PlayerSound) {
				PlayerSound.mute = bookIsOpen;
			}
		}

		private void DisablePlayerMovement() {
			if (CharacterController) {
				CharacterController.enabled = !bookIsOpen;
			}
			if (FPSControllerScript && bookIsOpen) {
				lastRunningSpeed = FPSControllerScript.m_RunSpeed;
				lastWalkingSpeed = FPSControllerScript.m_WalkSpeed;
				changeWalkingAndRunningSpeed (0, 0);
			} else {
				changeWalkingAndRunningSpeed (lastWalkingSpeed, lastRunningSpeed);		
			}
		}

		private void changeWalkingAndRunningSpeed(float walkingSpeed, float runningSpeed) {
			FPSControllerScript.m_RunSpeed = runningSpeed;
			FPSControllerScript.m_WalkSpeed = walkingSpeed;		
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
				Debug.Log (getAnimationDuration (anim, bookIsOpen ? "BFI" : "BF0"));
				yield return new WaitForSeconds (getAnimationDuration (anim, bookIsOpen?"BFI":"BFO"));
				Debug.Log (getAnimationDuration(anim,"BFO"));
				explBook.SetActive (bookIsOpen); //only matters when book is Open == false. 
				StoryTextLeft.SetActive (bookIsOpen);
				StoryTextRight.SetActive (bookIsOpen);
			}
		}

		private float getAnimationDuration(Animator a, string n) {
			foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
				if (clip.name == n) return clip.length;
			return 0;
		}

		private void depictExplorersStory() {
			if (!bookIsOpen) {
				currentStoryIndex = 0;
			}
			if (lvlManager) {
				ExplorersStory = lvlManager.getParagraphs ();
			}
			if (StoryTextRight && StoryTextLeft && bookIsOpen) {
				StoryTextLeft.SetActive (bookIsOpen);
				StoryTextRight.SetActive (bookIsOpen);
				SetText (StoryTextLeft.GetComponent<TextMesh> ());
				SetText (StoryTextRight.GetComponent<TextMesh> ());
			}
			currentStoryIndex = 0;
		}

		private void SetText(TextMesh textMesh) {
			if (textMesh && ExplorersStory != null) {
				textMesh.text = getNextPage(currentStoryIndex);
			}
			currentStoryIndex += 20;
		}

		private string appendLines(List<string> lines) {
			string result = "";
			foreach (string line in lines) { 
				result += line;
			}
			return result;
		}

		private string getNextPage(int currentIndex) {
			string result = "";
			if (ExplorersStory.Count - currentIndex >= 20) {
				result = appendLines (ExplorersStory.GetRange (currentIndex, 20));
			} else {
				result = appendLines ( ExplorersStory.GetRange(currentIndex, ExplorersStory.Count - currentIndex));
			} 
			return result;
		}

		void OnGUI() {
		}

        public bool isBookOpen()
        {
            return bookIsOpen;
        }
	}
}

