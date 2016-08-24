using UnityEngine;
using System.Collections;

public class ContinousCameraRotationMainMenu : MonoBehaviour {

	private Transform cameraTransform;
	// Use this for initialization
	void Start () {
		cameraTransform =  this.gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		cameraTransform.Rotate (new Vector3 (0, 0.1f, 0));
	}
}
