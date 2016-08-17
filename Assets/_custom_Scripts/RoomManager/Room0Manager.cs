using UnityEngine;
using System.Collections;
using System;

public class Room0Manager : MonoBehaviour
{
	private bool doorLeft = false;
	private bool doorVisited = false;

	GameObject player, door;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FirstPersonCharacter");
		door = GameObject.Find ("animated_door");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!doorLeft)
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorLeft = distance > 25;
			door.SetActive (doorLeft);
		}
		else if (!doorVisited)
		{
			float distance = Vector3.Distance (player.transform.position, door.transform.position);
			doorVisited = distance < 5;
			// open door
			Animator anim = door.GetComponent<Animator> ();
			if (anim) anim.SetBool ("open", doorVisited);
		}
	}
}
