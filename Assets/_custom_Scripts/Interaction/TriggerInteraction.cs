using UnityEngine;
using System.Collections;

/**
 * Interface for {@link TriggerInteraction}
 * @see Interaction.TriggerInteraction
 */
public abstract class TriggerInteractionCallback : MonoBehaviour {
	/** Use (trigger.triggerActive) for your validation */
	abstract public void OnTriggerInteraction (Interaction.TriggerInteraction trigger);
}

namespace Interaction
{
	/**
	 * Handle an interaction which is evaluated by a second script
	 */
	public class TriggerInteraction : PlainInteraction
	{
		/** Tells target script if it should be executed, has to be handled in the target script */
		public bool triggerActive = false;
		/** Attached script has to be conform to {@link TriggerInteractionCallback} interface */
		public TriggerInteractionCallback triggerScript;

		/**
		 * Will run the evaluation script and display the message (can be modified in the script)
		 */
		override public void OnInteractionKeyPressed() {
			XplrDebug.LogWriter.Write("trigger interaction with response: "+responseMessage, gameObject);
			if (triggerScript)
				triggerScript.OnTriggerInteraction (this);
			scriptedActionExecuter (responseMessage);
			centeredMessage (responseMessage);
		}
		/**
		 * Tags can be defined here for short usage like [leave_room]
		 */
		bool scriptedActionExecuter (string src) {
			if (src.Contains ("[leave_room]")) {
				StartCoroutine (FadeOutAndLeaveRoom ());
				responseMessage = responseMessage.Replace ("[leave_room]", "");
				return true;
			}
			if (src.Contains ("[main_menu]")) {
				LevelManager.LoadStartScreen ();
				responseMessage = responseMessage.Replace ("[main_menu]", "");
				return true;
			}
			return false;
		}
		/** Do the scene fading and load next room */
		IEnumerator FadeOutAndLeaveRoom(){
			SceneFadingScript sfs = Component.FindObjectOfType<SceneFadingScript> ();
			if (sfs) {
				float fadeTime = sfs.BeginFadeOut ();
				yield return new WaitForSeconds (fadeTime);
			} else {
				yield return null;
			}
			LevelManager.LeaveRoom ();
		}
	}
}