using UnityEngine;

public abstract class TriggerInteractionCallback : MonoBehaviour {
	abstract public void OnTriggerInteraction (Interaction.TriggerInteraction trigger);
}

namespace Interaction
{
	public class TriggerInteraction : PlainInteraction
	{
		public bool triggerActive = false;
		public TriggerInteractionCallback triggerScript;

		override public string interactMessage() {
			return actionMessage;
		}
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
			return false;
		}
	}
}