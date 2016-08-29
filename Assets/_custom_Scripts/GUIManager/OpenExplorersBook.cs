using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace ExplorersBook
{
	/**
	* Manages the Opening and Closing of the Book as well as the Input Field. 
	*/
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
		public bool shouldShowLeftPage = true;
		/**
		* Instantiate needed Classvariables and make sure Explorers Book and input Field are inactive
		*/
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

		/**
		* Listen for Escape and B to open/close ExplorersBook when pressed
		*/
		void LateUpdate() {
			if ((Input.GetKeyUp(KeyCode.B) && !bookIsOpen) || (Input.GetKeyUp(KeyCode.Escape) && bookIsOpen) ) {
				openExplorersBook ();
			}
		}

		/**
		* Call all neccessary Methods to Activate / Disable The Book and The Input Field as well as the Player Movement usw.
		*/
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

		/**
		* Activate Explorers Book and start the Coroutine for the Animation
		*/
		private void ActivateExplorersBook() {
			if (explBook && bookIsOpen) {
				explBook.SetActive (bookIsOpen);
				StartCoroutine (playAnimation ());
			} else {
				StartCoroutine(playAnimation ());
			}
		}

		/**
		* Activate the Input Field responsible for UserInput
		*/
		private void ActivateUserInputField() {
			if (inputField) {
				inputField.SetActive(bookIsOpen);
				InputField inputFieldComponent = inputField.GetComponent<InputField> ();
				inputFieldComponent.ActivateInputField ();
			}
		}

		/**
		* plays the Book Opening Animation. IEnummerator is used for the Coroutine. 
		*/
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

		/**
		* Returns the Duration of an Animation used in an Animator that has a specific Name. 
		*/
		private float getAnimationDuration(Animator animator, string name) {
			foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
				if (clip.name ==  name) return clip.length;
			return 0;
		}

		/**
		* Returns the Duration of an Animation used in an Animator that has a specific Name. 
		*/
		private void depictExplorersStory() {
			if (lvlManager) {
				ExplorersStory = lvlManager.getChapter ();
			}
			if (StoryTextRight && StoryTextLeft && bookIsOpen) {
				StoryTextLeft.SetActive (bookIsOpen);
				StoryTextRight.SetActive (bookIsOpen);
				if (shouldShowLeftPage) {
					SetText (StoryTextLeft.GetComponent<TextMesh> ());
				}
				SetText (StoryTextRight.GetComponent<TextMesh> ());
			}
			currentStoryIndex = 0;
		}

		/**
		* Sets the Text of a given textMesh to a Page text. 
		*/
		private void SetText(TextMesh textMesh) {
			if (textMesh && ExplorersStory != null) {
				textMesh.text = getNextPage(currentStoryIndex);
			}
			currentStoryIndex += 20;
		}

		/**
		* Appends the lines, wich fit to one Page, for the right Page and gives them back. 
		*/
		private string getNextPage(int currentIndex) {
			string result = "";

			if (ExplorersStory.Count < 20) {
				return appendLines(ExplorersStory.GetRange(0, ExplorersStory.Count));
			}

			if (ExplorersStory.Count - currentIndex >= 20) {
				result = appendLines (ExplorersStory.GetRange (currentIndex, 20));
			} else {
				result = appendLines (ExplorersStory.GetRange (currentIndex, ExplorersStory.Count - currentIndex));
			} 
			return result;
		}

		/**
		* Appends the Lines of an Array to a string.
		*/
		private string appendLines(List<string> lines) {
			string result = "";
			foreach (string line in lines) { 
				result += line;
			}
			return result;
		}

		/**
		* Tells you if the book is open. 
		*/
        public bool isBookOpen()
        {
            return bookIsOpen;
        }
	}
}