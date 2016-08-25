using UnityEngine;

namespace Interaction
{
	/**
	 * Switch materials for the VictorianLights asset
	 */
	public class VictorianLight : Interactable 
	{
		public Material LightsOn;
		public Material LightsOff;
		/** Tells which material to use and what message to display */
		public bool isLightOn = false;

		override public string interactMessage() {
			return (isLightOn ? "Turn off light" : "Turn on light");
		}
		/**
		 * Switches the material to indicate a turned on light
		 */
		override public void OnInteractionKeyPressed() {
			EnableGUI (false);
			isLightOn = !isLightOn;
			GetComponent<Renderer> ().material = (isLightOn ? LightsOn : LightsOff);
		}
	}
}