using UnityEngine;
using System.Collections;
using Interaction;

public class MouseCrosshair : MonoBehaviour, Interactable {
	public Texture2D CrosshairDot;
	public Texture2D CrosshairCircle;
	private Rect positionDot;
	private Rect positionCircle;
	private bool shouldChangeTexture;

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

	public void HandleRaycastCollission() {
		
	}

	public void EnableGUI(bool enable) {
		shouldChangeTexture = enable;
	}

	void OnGUI ()
	{
		GUI.DrawTexture (positionDot, CrosshairDot);
		if (shouldChangeTexture) {
			GUI.DrawTexture (positionCircle,  CrosshairCircle);
		}
	}

}