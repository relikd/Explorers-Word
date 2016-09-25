using UnityEngine;
using XplrDebug;
using System.Collections;

/** Contains all classes with which the user can interact */
namespace Interaction
{
	/**
	 * Handle an interaction which starts an attached animation
	 */
	public class AnimationInteraction : PlainInteraction
	{
		public Animator animationToPlay;

		/**
		 * Will run the attached animator, wich hopefully contains an Animation that is handle over a State machine, and display the message
		 */
		override public void OnInteractionKeyPressed() {

			LogWriter.Write ("Animation abgespielt: " + animationToPlay.name, this.gameObject);
			StartCoroutine (playAnimation());
			centeredMessage (responseMessage);
			interactionEnabled = false;

		}
	
	/**
	 * Plays the Animation of the attached Animator and waits while its playing
	 * @return duration in seconds
	 */
	IEnumerator playAnimation() {
		Animator anim = this.animationToPlay;
		if (anim) {
			anim.SetBool ("animate", true);
			yield return new WaitForSeconds (getAnimationDuration (anim, "AnimationPillar"));
			//anim.SetBool ("Trigger", false); // dont reset. should be animated once			
		}
		yield return new WaitForSeconds (0.3f);
	}

	/**
	 * Duration between the Pillar-Animations endresult and its original position
	 * @return duration in seconds
	 */
	private float getAnimationDuration(Animator a, string n) {
		foreach (AnimationClip clip in a.runtimeAnimatorController.animationClips)
			if (clip.name == n) return clip.length;
		return 0;
	}
}
}