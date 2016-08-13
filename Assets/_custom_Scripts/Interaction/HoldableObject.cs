using UnityEngine;
using System.Collections;
using Interaction;

public class HoldableObject : Interactable
{
	public GameObject target;

	override public string interactMessage() {
		return "hold";
	}

	override public void HandleRaycastCollission() {
		if (Input.GetKeyUp (theKeyCode())) {
			HandleRigidBody (true);		
			Pickup ();
		}
	}

	void Update() {
		if (Input.GetKeyDown (theKeyCode())) {
			Drop ();
			HandleRigidBody (false);
		}
	}

	void Pickup() {
		this.transform.position = this.target.transform.position;
//		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = GameObject.Find ("FirstPersonCharacter").transform;
	}

	void Drop() {
//		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = null;
	}

	private void HandleRigidBody(bool isKinematic) {
		if (gameObject.GetComponent<Rigidbody> ()) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = isKinematic;
		}
	}
}