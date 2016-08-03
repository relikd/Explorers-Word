
using UnityEngine;
using UnityEditor;
using System.Collections;

public class VictorianLight : MonoBehaviour 
{
	

	public Material LightsOn;
	public Material LightsOff;

	[HideInInspector] public bool Running = false;

	private GameObject gaga;
	VictorianLight() {
		
	}

	void Update() {
	}

	void Start() {
		enabled = true;

		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
		Renderer rend = go.GetComponent<Renderer>();
		//this. = Resources.Load("Victorian Streetlight/Material/Streetlight_On") as Material;


		gaga = Instantiate(Resources.Load("Victorian Streetlight/Prefabs/Streetlight_00_on", typeof(GameObject))) as GameObject;

	}

	public void TurnLightsOn() {
		this.GetComponent<Renderer> ().material = LightsOn;
//		this.gaga.GetComponent<Renderer>().material.mainTexture
	}
	
	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		TurnLightOn detection = Player.GetComponent<TurnLightOn>();

		if (detection.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to turn on / off");
		}
	}
}
