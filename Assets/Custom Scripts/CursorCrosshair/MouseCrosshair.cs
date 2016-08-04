using UnityEngine;
using System.Collections;

public class MouseCrosshair : MonoBehaviour {
	public Texture2D CrosshairDot;
	public Texture2D CrosshairCircle;
	private Rect positionDot;
	private Rect positionCircle;

	// Use this for initialization
	void Start () {
		
		positionDot = new Rect((Screen.width - CrosshairDot.width) / 2, (Screen.height - 
			CrosshairDot.height) /2, CrosshairDot.width, CrosshairDot.height);
		positionCircle = new Rect((Screen.width - CrosshairDot.width*2) / 2, (Screen.height - 
			CrosshairDot.height*2) /2, CrosshairDot.width*2, CrosshairDot.height*2);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI ()
	{
		GUI.DrawTexture (positionDot, CrosshairDot);
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		TurnLightOn detection = Player.GetComponent<TurnLightOn>();

		if (detection.InReach == true) {
			GUI.DrawTexture (positionCircle,  CrosshairCircle);
		}
	}

}