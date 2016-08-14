using UnityEngine;
using System.Collections;

public class Puzzle1BookHandler : TriggerInteractionCallback
{
	private bool shelfOpen = false;
	private float shelfRotationAngle = 0.0f;
	
	override public void OnTriggerInteraction (TriggerInteraction sender) {
		if (sender.triggerActive) {
			sender.responseMessage = "A rusty mechanism moved the bookshelf";
			StartCoroutine (playAnimation ());
		}
	}

	IEnumerator playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		if (anim) {
			anim.SetBool ("Trigger", true);
			yield return new WaitForSeconds (getAnimationDuration (anim, "BookInTheShelveAnimation"));
			//anim.SetBool ("Trigger", false); // dont reset. should be animated once
		}
		shelfOpen = true;
	}

	void Update() {
		if (shelfOpen && shelfRotationAngle > -17.0f) {
			// TODO: play sound 'stone on stone scrape'
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
