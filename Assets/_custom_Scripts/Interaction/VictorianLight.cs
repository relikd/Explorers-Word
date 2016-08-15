using UnityEngine;
using System.Collections;
using Interaction;

public class VictorianLight : Interactable 
{
	public Material LightsOn;
	public Material LightsOff;

	override public string interactMessage() {
		return "turn on / off";
	}

	override public void OnInteractionKeyPressed() {
		if (GetComponent<Renderer> ().material.name.Contains ("Streetlight_Off")) {
			GetComponent<Renderer> ().material = LightsOn;
		} else {
			GetComponent<Renderer> ().material = LightsOff;
		}
	}
}
