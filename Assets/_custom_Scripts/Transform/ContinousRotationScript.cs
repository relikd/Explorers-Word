using UnityEngine;
using System.Collections;

public class ContinousRotationScript : MonoBehaviour {

	public Vector3 rotationAxis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Transform> ().Rotate (rotationAxis);
	}
}
