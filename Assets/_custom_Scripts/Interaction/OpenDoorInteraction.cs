
using UnityEngine;
using System;

public class OpenDoorInteraction : Interaction.Interactable
{
	void LateUpdate() {
		
	}

	void Awake() {
		PlainInteraction PlainInteraction = GameObject.Find ("pCube1").GetComponent<PlainInteraction> ();
		Destroy (PlainInteraction);
	}

	public override string interactMessage ()
	{
		return "Open Door With Key";	
	}

	public override void OnInteractionKeyPressed ()
	{
		
	}

}

