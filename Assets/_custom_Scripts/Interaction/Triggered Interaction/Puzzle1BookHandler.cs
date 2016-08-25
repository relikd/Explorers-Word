	using UnityEngine;
using System.Collections;

public class Puzzle1BookHandler : TriggerInteractionCallback
{
	private bool shelfOpen = false;
	private float shelfRotationAngle = 0.0f;
	public GameObject HiddenEntrance;
	public GameObject EntranceWall;

	override public void OnTriggerInteraction (Interaction.TriggerInteraction sender) {
		if (sender.triggerActive) {
			sender.responseMessage = "A rusty mechanism moved the bookshelf";
			StartCoroutine (playAnimation ());
			HiddenEntrance.SetActive(true);
			EntranceWall.SetActive (false);
		}
	}

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
	}

	void Update() {
		if (shelfOpen && shelfRotationAngle > -17.0f) {
			GameObject bsrc = GameObject.Find ("BookShelfRotationContainer");
			if (bsrc) {
				shelfRotationAngle -= 15.0f * Time.deltaTime;
				bsrc.transform.eulerAngles = new Vector3 (0, shelfRotationAngle, 0);
			}
		}
	}

	private float getAnimationDuration(Animator a, string n) {
		foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
			if (clip.name == n) return clip.length;
		return 0;
	}
}
