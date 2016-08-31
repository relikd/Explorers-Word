using UnityEngine;
using System.Collections;

/**
 * Handles Maya book activation and animations for the book and the bookshelve
 */
public class Puzzle1BookHandler : TriggerInteractionCallback
{
	/** ShelfOpen == true means puzzle is solved */
	private bool shelfOpen = false;
	private float shelfRotationAngle = 0.0f;
	public GameObject HiddenEntrance;
	public GameObject EntranceWall;

	/**
	 * Will be called from a {@link TriggerInteraction} but still have to validate if trigger is active
	 */
	override public void OnTriggerInteraction (Interaction.TriggerInteraction sender) {
		if (sender.triggerActive) {
			sender.responseMessage = "Das Bücherregal bewegt sich";
			StartCoroutine (playAnimation ());
			HiddenEntrance.SetActive(true);
			EntranceWall.SetActive (false);
		}
	}
	/**
	 * Play book pull animation
	 */
	IEnumerator playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		GameObject shelf = GameObject.Find ("Bookshelf");
		if (anim) {
			anim.SetBool ("Trigger", true);
			yield return new WaitForSeconds (getAnimationDuration (anim, "BookInTheShelveAnimation"));
			//anim.SetBool ("Trigger", false); // dont reset. should be animated once
			shelf.GetComponent<AudioSource> ().Play ();
		}
		shelfOpen = true;
		yield return new WaitForSeconds (4.0f);
		GlobalSoundPlayer.playPuzzleSolved ();
	}
	/**
	 * Play opening bookshelve animation
	 */
	void Update() {
		if (shelfOpen && shelfRotationAngle > -17.0f) {
			GameObject bsrc = GameObject.Find ("BookShelfRotationContainer");
			if (bsrc) {
				shelfRotationAngle -= 15.0f * Time.deltaTime;
				bsrc.transform.eulerAngles = new Vector3 (0, shelfRotationAngle, 0);
			}
		}
	}
	/**
	 * Duration between book pull and book returning to its original position
	 * @return duration in seconds
	 */
	private float getAnimationDuration(Animator a, string n) {
		foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
			if (clip.name == n) return clip.length;
		return 0;
	}
}
