﻿
using UnityEngine;
using UnityEditor;
using System.Collections;

public class VictorianLight : MonoBehaviour 
{
	public Material LightsOn;
	public Material LightsOff;

	[HideInInspector] public bool Running = false;
	private string LightsOnName = "Streetlight_On";
	private string LightsOffName = "Streetlight_Off";

	void Update() {
	}

	void Start() {
		enabled = true;
	}

	public void TurnLightsOn() {
		//Material currentMaterial = this.GetComponent<Renderer> ().material;
		print(GetComponent<Renderer> ().material.name);

		if (GetComponent<Renderer> ().material.name.Contains("Streetlight_Off")) {
			GetComponent<Renderer> ().material = LightsOn;
			Running = true;
		} else {
			GetComponent<Renderer> ().material = LightsOff;
			Running = true;
		}
		Running = false;
	}
	
	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		TurnLightOn detection = Player.GetComponent<TurnLightOn>();

		if (detection.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), "Press 'E' to turn on / off");
		}
	}
}
