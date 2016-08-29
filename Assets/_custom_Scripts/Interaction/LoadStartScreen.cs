using System;
using UnityEngine;
using Interaction;

namespace Interaction
{
	/**
	 * Replace {@link PlainInteraction} with this open door script
	 */
	public class LoadStartScreen : PlainInteraction
	{
		/**
		 * Removes the assigned {@link PlainInteraction} on initialize
		*/
		void Awake() {
		}

		public override string interactMessage () {
			return responseMessage;
		}

		public override void OnInteractionKeyPressed () {
			LevelManager.LoadStartScreen();
		}
	}
}


