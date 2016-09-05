using UnityEngine;

namespace Interaction
{
	/**
	 * Rotate an object by pre defined {@link Vector3} euler angles. Can be either continuously or stepwise
	 */
	public class RotateInteraction : Interactable
	{
		/** Euler Angles used for rotation, can be negative */
		public Vector3 rotateBy = new Vector3(0,10,0);
		/** Define if rotate should happen on key press or continuously */
		[SerializeField] private bool stepAngle = false;

		override public string interactMessage() {
			return "drehen";
		}
		/**
		 * Rotate one time on key press if {@link #stepAngle} == true
		 */
		override public void OnInteractionKeyPressed() {
			if (stepAngle) rotate ();
		}
		/**
		 * Rotate constantly if {@link #stepAngle} == false
		 */
		override public void OnInteractionKeyHold() {
			if (!stepAngle) rotate ();
		}
		/**
		 * Rotate object by defined euler angles
		 */
		void rotate() {
			gameObject.transform.Rotate (rotateBy);
			checkForRotationLimit ();
			XplrDebug.LogWriter.Write ("Rotated Objekt by " + rotateBy, gameObject);
		}
		/**
		 * Will call the {@link RotationLimiter} Extension to check for any limitation
		 * @see RotationLimiter
		 */
		void checkForRotationLimit() {
			RotationLimiter rl_script = gameObject.GetComponent<RotationLimiter>();
			if (rl_script) {
				int ool = rl_script.outOfLimit ();
				int dol = rl_script.directlyOnLimit ();
				int edge = ool | dol;
				if ((edge & 1)!=0) rotateBy.x *= -1;
				if ((edge & 2)!=0) rotateBy.y *= -1;
				if ((edge & 4)!=0) rotateBy.z *= -1;

				if (ool > 0) {
					gameObject.transform.Rotate (rotateBy);
					if (rl_script.directlyOnLimit () > 0)
						gameObject.transform.Rotate(rotateBy);
				}
			}
		}
	}
}