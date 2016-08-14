using UnityEngine;
using System.Collections;

public class Puzzle1BookHandler : TriggerInteractionCallback
{
	private bool shelfOpen = false;
	private float shelfRotationAngle = 0.0f;
	
	override public void OnTriggerInteraction (TriggerInteraction sender) {
		if (sender.triggerActive) {
			playAnimation ();
			// TODO: wait till animation finished
			shelfOpen = true;
			sender.responseMessage = "A rusty mechanism moved the bookshelf";
		}
	}

	private void playAnimation() {
		Animator anim = gameObject.GetComponent<Animator> ();
		if (anim) {
			anim.SetBool ("Trigger", true);
		}
	}

	void Update() {
		if (shelfOpen && shelfRotationAngle > -17.0f) {
			GameObject bsrc = GameObject.Find ("BookShelfRotationContainer");
			if (bsrc) {
				shelfRotationAngle -= 10.0f * Time.deltaTime;
				bsrc.transform.eulerAngles = new Vector3 (0, shelfRotationAngle, 0);
			}
		}
	}
}
