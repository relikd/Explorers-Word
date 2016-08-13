using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	static LevelManager Instance;

	void Start () {
		if (Instance != null)
			GameObject.Destroy (gameObject);
		else {
			GameObject.DontDestroyOnLoad (gameObject);
			Instance = this;
		}
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("room_1");
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("room_2");
		}
	}
}
