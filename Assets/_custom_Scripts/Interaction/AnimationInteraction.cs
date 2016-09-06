using UnityEngine;
using XplrDebug;

namespace Interaction
{
	/**
	 * Handle an interaction which is evaluated by a second script
	 */
	public class AnimationInteraction : PlainInteraction
	{
		public Animation animationToPlay;
		public bool animateOnce;
		public bool deactColliderAfterwards;
		public Collider colliderToDeactivate;

		/**
		 * Will run the attached animation and display the message
		 */
		override public void OnInteractionKeyPressed() {

			LogWriter.Write ("Animation abgespielt: " + animationToPlay.name, this.gameObject);
			animationToPlay.Play ();

			if (animateOnce)
				this.interactionEnabled = false;
			if (deactColliderAfterwards) {
				colliderToDeactivate.enabled = false;
			}
			centeredMessage (responseMessage);
		}
	}
}