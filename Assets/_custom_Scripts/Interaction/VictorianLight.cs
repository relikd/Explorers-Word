
using UnityEngine;
using UnityEditor;
using System.Collections;
using Interaction;

public class VictorianLight : MonoBehaviour, Interactable 
{
	public Material LightsOn;
	public Material LightsOff;

	void Update() {
	}

	void Start() {
		enabled = true;
	}

	public void HandleRaycastCollission() {
		if (Input.GetKeyUp (KeyCode.E)) {
			if (GetComponent<Renderer> ().material.name.Contains ("Streetlight_Off")) {
				GetComponent<Renderer> ().material = LightsOn;
			} else {
				GetComponent<Renderer> ().material = LightsOff;
			}
		}
	}

	public void EnableGUI(bool enable) {
		GameObject player = GameObject.Find ("FirstPersonCharacter");
		if (player) {
			player.GetComponent<GUIManager> ().register ("Press 'E' to turn on / off", enable);
		}
	}

	void OnGUI ()
	{

	}
}
