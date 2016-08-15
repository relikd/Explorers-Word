﻿using UnityEngine;
using System.Collections;
using Interaction;

public class CollectInteraction : Interactable
{
	override public string interactMessage() {
		return "take";
	}

	override public void OnInteractionKeyPressed () {
		this.gameObject.SetActive(false);
	}
}