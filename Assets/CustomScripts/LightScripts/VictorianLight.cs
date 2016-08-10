
using UnityEngine;
using UnityEditor;
using System.Collections;
using Interaction;

public class VictorianLight : MonoBehaviour, Interactable 
{
	public Material LightsOn;
	public Material LightsOff;

	[HideInInspector] public bool shouldDisplayText = false;

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
		shouldDisplayText = enable;
	}

	void OnGUI ()
	{
		if (shouldDisplayText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), "Press 'E' to turn on / off");
		}
	}
}
