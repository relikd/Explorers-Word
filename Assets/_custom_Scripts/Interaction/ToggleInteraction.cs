using UnityEngine;
using System;

namespace Interaction
{
	/**
	 * Toggles the assigned scripts active property
	 */
	public class ToggleInteraction : PlainInteraction {
		/** Possible interaction outcome */
		[Serializable]
		enum OnInteractionOperation {
			DoNothing,
			DeleteGameObject,
			CollectGameObject
		}
		/** Defines what should happen after an interaction was triggered */
		[SerializeField] private OnInteractionOperation onInteraction;
		/** The list of scripts which should be toggled */
		[Tooltip("for single usage: add this script to the list below")]
		[SerializeField] private Interactable[] toggleScriptsEnabledState;

		/**
		 * Toggles all assigned interaction scripts and displays a message (eventually delete object)
		 */
		override public void OnInteractionKeyPressed() {
			centeredMessage (responseMessage);

			foreach (Interactable script in toggleScriptsEnabledState)
				script.interactionEnabled = !script.interactionEnabled;

			switch (onInteraction) {
			case OnInteractionOperation.CollectGameObject:
			case OnInteractionOperation.DeleteGameObject:
				Destroy (gameObject);
				break;
			default:
				break;
			}
		}
	}
}