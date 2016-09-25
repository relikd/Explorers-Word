﻿using UnityEngine;
using System.Collections;

/**
 * Used to shatter the planks in room 4. Needs the remains of the breakable Object and the object which triggers the script.
 */ 
public class breakableChest : Breakable {

	/// <summary>
	/// The object which triggers the script.
	/// </summary>
	[SerializeField]
	GameObject BreakingObject;
	/**
	 * Checks if the collision was with the given "BreakingObject".
	 */
	public void OnCollisionEnter(Collision col) {
		if (col.gameObject == BreakingObject) {
			shatter ();
		}
	}
}
