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
		public GameObject explBook;
		public GameObject inputField;
		private GameObject StoryTextLeft;
		private GameObject StoryTextRight;
		private LevelManager lvlManager;
		private bool bookIsOpen = false;
		private List<string> ExplorersStory = new List<string>();
		private int currentStoryIndex = 0;
		public bool shouldShowBook = true;

		void Awake() {
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

		void LateUpdate() {
			if ((Input.GetKeyUp(KeyCode.B) && !bookIsOpen) || (Input.GetKeyUp(KeyCode.Escape) && bookIsOpen) ) {
				openExplorersBook ();
			}
		}

		public void openExplorersBook() { 
			if (shouldShowBook) {
				GameManager gameManager = GameManager.getInstance ();
				bookIsOpen = !bookIsOpen;
				ActivateExplorersBook ();
				gameManager.disablePlayerAudioSource ();
				gameManager.disableWalking ();
				gameManager.disableJumping ();
				gameManager.disableCrosshair ();
				ActivateUserInputField ();
				depictExplorersStory ();
			}
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
			
		IEnumerator playAnimation() {
			Animator anim = explBook.GetComponent<Animator> ();
			if (anim) {
				anim.SetBool ("open", bookIsOpen);
				Debug.Log (getAnimationDuration (anim, bookIsOpen ? "BFI" : "BF0"));
				yield return new WaitForSeconds (getAnimationDuration (anim, bookIsOpen?"BFI":"BFO"));
				Debug.Log (getAnimationDuration(anim,"BFO"));
				explBook.SetActive (bookIsOpen); //only matters when book is Open == false. 
				StoryTextLeft.gameObject.SetActive (bookIsOpen);
				StoryTextRight.gameObject.SetActive (bookIsOpen);
			}
		}

		private float getAnimationDuration(Animator a, string n) {
			foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
				if (clip.name == n) return clip.length;
			return 0;
		}

		private void depictExplorersStory() {
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

		private string getNextPage(int currentIndex) {
			string result = "";
			if (ExplorersStory.Count - currentIndex >= 20) {
				result = appendLines (ExplorersStory.GetRange (currentIndex, 20));
			} else {
				result = appendLines ( ExplorersStory.GetRange(currentIndex, ExplorersStory.Count - currentIndex));
			} 
			return result;
		}

		private string appendLines(List<string> lines) {
			string result = "";
			foreach (string line in lines) { 
				result += line;
			}
			return result;
		}

        public bool isBookOpen()
        {
            return bookIsOpen;
        }
	}
}