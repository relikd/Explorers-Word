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

	static public void LoadNextRoom() {
		MonoBehaviour[] allScripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
		for (int i = allScripts.Length; i > 0; i--) {
			if (allScripts[i-1] != Instance) {
				Destroy (allScripts[i-1]);
			}
		}

		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		levelNumber++;
		SceneManager.LoadScene ("room_"+levelNumber, LoadSceneMode.Single);
	}
}
