
using UnityEngine;
using UnityEditor;
using System.Collections;

public class HoldableObject : MonoBehaviour 
{
	public GameObject target;
	public string TriggerTag;
	//[HideInInspector] public bool Running = false;
	[HideInInspector] public bool currentlyPicked;

	void Update() {

	}

	void Start() {
		enabled = true;
	}

	 void Pickup() {
		this.transform.position = this.target.transform.position;
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = GameObject.Find ("FirstPersonCharacter").transform;
		currentlyPicked = true;
	}

	void Drop() {
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = null;
		currentlyPicked = false;
	}

	void LateUpdate() {
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();


		if (Input.GetKeyUp (KeyCode.E) ) {
			if (currentlyPicked == false && detection.RaycastHit.collider.tag == TriggerTag && detection.InReach == true) {
				HandleRigidBody (true);
				Pickup ();
			} else {
				HandleRigidBody (false);
				Drop();
			}
		}
	}

	private void HandleRigidBody(bool isKinematic) {
		if (gameObject.GetComponent<Rigidbody> ()) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = isKinematic;
		}
	}

	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();

		if (detection.InReach == true && detection.RaycastHit.collider.tag == gameObject.tag)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to hold / drop");
		}
	}
}
