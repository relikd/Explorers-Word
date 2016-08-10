
using UnityEngine;
using UnityEditor;
using System.Collections;

public class HoldableObject : MonoBehaviour, Interactable
{
	public GameObject target;
	public string TriggerTag;
	[HideInInspector]public bool shouldDepictText;

	void Update() {

	}

	void Start() {
		enabled = true;
	}

	void LateUpdate() {
		if (Input.GetKeyDown (KeyCode.E) && shouldDepictText == false) {
			Drop ();
			HandleRigidBody (false);
		}
	}

	public void HandleRaycastCollission() {
		HandleRigidBody (true);		
		Pickup ();
	}

	void Pickup() {
		this.transform.position = this.target.transform.position;
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = GameObject.Find ("FirstPersonCharacter").transform;
		shouldDepictText = false;
	}

	void Drop() {
		this.transform.parent = GameObject.Find ("FPSController").transform;
		this.transform.parent = null;
		shouldDepictText = false;
	}

	private void HandleRigidBody(bool isKinematic) {
		if (gameObject.GetComponent<Rigidbody> ()) {
			gameObject.GetComponent<Rigidbody> ().isKinematic = isKinematic;
		}
	}

	public void EnableGUI() {
		shouldDepictText = true;
	}

	void OnGUI ()
	{
		if (shouldDepictText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to hold / drop");
					
		}
	}
}
