using System;
using Interaction;

public class RemoveScrews : Interactable
{
	void Awake() {

	}

	public override string interactMessage () {			
		return "Remove Screws";
	}
		
	public override void OnInteractionKeyPressed () {
		this.interactionEnabled = false;
		//LevelManager.LoadNextRoom ();
	}
}


