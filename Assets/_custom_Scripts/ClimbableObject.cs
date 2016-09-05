using UnityEngine;
using System.Collections;

public class ClimbableObject : MonoBehaviour {

	public GameObject player;
	public GameObject ground;
	public XplrCharacter.FPSController fps_controller;

	private BoxCollider climbCollider;

	private bool onWall = false;

	private float distanceToGround;

	// Use this for initialization
	void Start () {
		climbCollider = this.gameObject.GetComponent<BoxCollider> ();
		climbCollider.isTrigger = true;
	}

	void Update(){
		distanceToGround = Vector3.Distance (player.transform.position, ground.transform.position);
		if (distanceToGround < 0.1) {
			onWall = false;
		}
	}

	void FixedUpdate(){

		if (onWall && Input.GetKey (KeyCode.W)) {
			player.GetComponent<Transform> ().position += new Vector3 (0, 0.05f, 0);
		}
		if (onWall && Input.GetKeyUp (KeyCode.Space)) {
			player.GetComponent<Transform> ().position += new Vector3 (0, 0, 0.5f);
			onWall = false;
		}
		if (onWall && distanceToGround > 0.2 && Input.GetKey(KeyCode.S)) {
			player.GetComponent<Transform> ().position -= new Vector3 (0, 0.05f, 0);
		}
	}

	void OnTriggerEnter(Collider col){
		player.GetComponent<Rigidbody> ().mass = 0.0f;
		onWall = true;
		fps_controller.shouldJump = false;
		fps_controller.shouldWalk = false;
		fps_controller.shouldPlayAudioSounds = false;
		fps_controller.m_UseHeadBob = false;
	}

	void OnTriggerExit(Collider col){
		player.GetComponent<Rigidbody> ().mass = 1.0f;
		onWall = true;
		fps_controller.shouldJump = true;
		fps_controller.shouldWalk = true;	
		fps_controller.shouldPlayAudioSounds = true;
		fps_controller.m_UseHeadBob = true;
	}

}
