using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 positionPlayer = gameObject.transform.position;
		if (positionPlayer.z > 10) {
			SceneManager.LoadScene ("room_0");
		}	
	}
}
