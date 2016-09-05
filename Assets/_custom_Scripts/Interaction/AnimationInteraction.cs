using UnityEngine;

namespace Interaction
{
	/**
	 * Handle an interaction which is evaluated by a second script
	 */
	public class AnimationInteraction : PlainInteraction
	{
		public Animation animationToPlay;
		public bool animateOnce;
		public bool deactCollider;
		public Collider collider;

		/**
		 * Will run the attached animation and display the message
		 */
		override public void OnInteractionKeyPressed() {

			animationToPlay.Play ();

			if (animateOnce)
				this.interactionEnabled = false;
			if (deactCollider) {
				collider.enabled = false;
			}
			centeredMessage (responseMessage);
		}
	}
}