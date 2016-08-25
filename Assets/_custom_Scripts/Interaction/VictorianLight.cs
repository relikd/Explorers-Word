using UnityEngine;

namespace Interaction
{
	public class VictorianLight : Interactable 
	{
		public Material LightsOn;
		public Material LightsOff;

		override public string interactMessage() {
			return "turn on / off";
		}
		/**
		 * Switches the material to indicate a switched on light
		 */
		override public void OnInteractionKeyPressed() {
			if (GetComponent<Renderer> ().material.name.Contains ("Streetlight_Off")) {
				GetComponent<Renderer> ().material = LightsOn;
			} else {
				GetComponent<Renderer> ().material = LightsOff;
			}
		}
	}
}