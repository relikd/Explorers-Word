
using UnityEngine;
using UnityEditor;
using System.Collections;


namespace Interaction {
public class HoldableObject : MonoBehaviour, Interactable
{
	public GameObject target;
	[HideInInspector]public bool shouldDepictText;

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
			shouldDepictText = enable;
	}

	void OnGUI ()
	{
		if (shouldDepictText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to h / d");
					
		}
	}
}
}