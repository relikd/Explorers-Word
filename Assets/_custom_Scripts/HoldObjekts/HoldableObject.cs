
using UnityEngine;
using UnityEditor;
using System.Collections;


namespace Interaction {
public class HoldableObject : MonoBehaviour, Interactable
{
	public GameObject target;

	void Update() {

	}

	void Start() {
		enabled = true;
	}

	void LateUpdate() {
		if (Input.GetKeyDown (KeyCode.E)) {
			Drop ();
			HandleRigidBody (false);
		}
	}

	public void HandleRaycastCollission() {
		if (Input.GetKeyUp (KeyCode.E)) {
			HandleRigidBody (true);		
			Pickup ();
		}
	}

	void Pickup() {
		this.transform.position = this.target.transform.position;
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = GameObject.Find ("FirstPersonCharacter").transform;
		
	}

	void Drop() {
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = null;
		
	}

	private void HandleRigidBody(bool isKinematic) {
		if (gameObject.GetComponent<Rigidbody> ()) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = isKinematic;
		}
	}

	public void EnableGUI(bool enable) {
			GameObject player = GameObject.Find ("FirstPersonCharacter");
			player.GetComponent<GUIManager> ().register ("Press 'E' to h / d", enable);
	}

	void OnGUI ()
	{
	}
}
}