using UnityEngine;
using System;
using System.Collections;

namespace Interaction
{
	/**
	 * Can toggle Interactable.interactionEnabled and GameObject.activeSelf property
	 */
	public class ToggleInteraction : PlainInteraction {
		/** Possible interaction outcome after toggle */
		[Serializable]
		enum OnInteractionOperation {
			DoNothing,
			DeactivateThisScript,
			DeleteGameObject,
			CollectGameObject
		}
		/** Defines what should happen after an interaction was triggered */
		[SerializeField] private OnInteractionOperation onInteraction;
		/** The list of scripts which should be toggled */
		[SerializeField] private Interactable[] toggleScriptsEnabledState;
		/** A list of GameObject to be turned on or off {@link #setActive()} */
		[SerializeField] private GameObject[] toggleGameObjectActiveState;

		/**
		 * Display message and run Coroutine {@link #toggleScriptsAfter}
		 */
		override public void OnInteractionKeyPressed() {
			centeredMessage (responseMessage);
			StartCoroutine (toggleScriptsAfter());
		}
		/**
		 * Run the script toggle after 0.01 seconds so that it will not trigger {@link Interactable#OnInteractionKeyPressed()}
		 * Then evaluate any changes for this GameObject to be made (delete, collect, ...)
		 */
		IEnumerator toggleScriptsAfter() {
			// wait to suppress multiple interaction on same gaming object
			yield return new WaitForSeconds (0.01f);

			foreach (Interactable script in toggleScriptsEnabledState)
				if (script)
					script.interactionEnabled = !script.interactionEnabled;
			foreach (GameObject go in toggleGameObjectActiveState)
				if (go)
					go.SetActive (!go.activeSelf);
			
			// have to deactivate the old message in case it will be deactivated or deleted
			// but will be set on true anyway if it is still in reach
			EnableGUI (false);
			// what happens after the toggle
			switch (onInteraction) {
			case OnInteractionOperation.CollectGameObject:
			case OnInteractionOperation.DeleteGameObject:
				Destroy (gameObject);
				break;
			case OnInteractionOperation.DeactivateThisScript:
				this.interactionEnabled = false;
				break;
			}
		}
	}
}