using UnityEngine;

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
				LevelManager.LoadNextRoom ();
				responseMessage = responseMessage.Replace ("[leave_room]", "");
				return true;
			}
			if (src.Contains ("[load_cutscene]")) {
				LevelManager.LoadRoom ("cutscene");
				responseMessage = responseMessage.Replace ("[load_cutscene]", "");
				return true;
			}
			return false;
		}
	}
}