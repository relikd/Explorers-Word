
using UnityEngine;
using UnityEditor;
using System.Collections;

public class VictorianLight : MonoBehaviour 
{
	[HideInInspector] public bool Running = false;

	private Material material;

	VictorianLight() {
		
	}

	void Update() {
	}

	void Start() {
		enabled = true;
		material = Resources.Load("Materials/Streetlight_On", typeof(Material)) as Material;
	}

	public void TurnLightsOn() {
		GetComponent<Renderer> ().material = this.material;
	
	}
	
	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		TurnLightOn detection = Player.GetComponent<TurnLightOn>();

		if (detection.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Press 'E' to open/close");
		}
	}
}
