using UnityEngine;
using System.Collections;

namespace XplrGUI
{
	/**
	* Manages the Opening and Closing of the Book
	*/
	public class BookController : MonoBehaviour
	{
		static public bool disableBook = false;
		public GameObject explorersBook;
		public GameObject planeLeftPage;
		public GameObject planeRightPage;
		public Texture textureLeftPage;
		public Texture textureRightPage;

		static private bool bookIsOpen = false;

		/**
		 * Public getter used in {@link UserInput.setUserInputEnabled(bool)}
		 */
		public static bool isBookOpen() {
			return bookIsOpen;
		}
		/**
		* Set assigned Texture and make sure Explorers Book is inactive
		*/
		void Start() {
			setTextureForPage (textureLeftPage, planeLeftPage);
			setTextureForPage (textureRightPage, planeRightPage);
			if (explorersBook)
				explorersBook.SetActive (false);
		}
		/**
		* Listen for key press (B) and open/close the ExplorersBook
		*/
		void LateUpdate() {
			if (Input.GetButtonUp ("Book"))
				openExplorersBook (!bookIsOpen);
		}
		/**
		* Call all neccessary methods to de-/activate the book and player movement
		*/
		public void openExplorersBook(bool flag) { 
			LogWriter.WriteLog("Opened The book: " + flag + "; Should depict the Book: " + !disableBook, gameObject);
			if (!disableBook) {
				bookIsOpen = flag;
				StartCoroutine(playAnimation ());
				Reachable.shouldRaycast = !flag;
				MouseCrosshair.showCrosshair = !flag;
			}
		}
		/**
		* Play the book opening animation. IEnummerator is used for the Coroutine. 
		*/
		IEnumerator playAnimation() {
			if (explorersBook) {
				explorersBook.SetActive (true);
				Animator anim = explorersBook.GetComponent<Animator> ();
				if (anim) {
					anim.SetBool ("open", bookIsOpen);
					yield return new WaitForSeconds (getAnimationDuration (anim, bookIsOpen?"BFI":"BFO"));
				}
				explorersBook.SetActive (bookIsOpen); //only matters when book is Open == false. 
			}
		}
		/**
		* Return the Duration of an Animation used in an Animator that has a specific Name. 
		*/
		private float getAnimationDuration(Animator animator, string name) {
			foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
				if (clip.name == name)
					return clip.length * 0.5f; // because speed is 2
			return 0;
		}
		/**
		 * Checks if both are hooked up and assign the new Texture
		 */
		private void setTextureForPage (Texture texture, GameObject plane) {
			if (plane) {
				plane.SetActive (false);
				if (texture) {
					Renderer r = plane.GetComponent<Renderer> ();
					r.material.SetTexture ("_MainTex", texture);
					plane.SetActive (true);
				}
			}
		}
	}
}