using UnityEngine;
using System.Collections;

namespace Interaction
{
	/**
	 * Pushes an object upon interaction with predefined force
	 */
	public class ApplyForceInteraction : Interactable 
	{
		/// <summary>
		/// The force to apply in form of a Vector3.
		/// </summary>
		public Vector3 ForceToApply;
		private Rigidbody body;

		[SerializeField] string actionMessage;

		// Use this for initialization
		void Start () {
			body = this.gameObject.GetComponent<Rigidbody> ();
		}

		/**
		 * Returns the interaction text which is displayed when the player hovers over the object.
		 */
		override public string interactMessage() {
			return actionMessage;
		}

		/**
		 * Applies the external force to the object when the interaction key is pressed.
		 */
		override public void OnInteractionKeyPressed()
		{
			body.AddForce (ForceToApply);
		}
	}
}

