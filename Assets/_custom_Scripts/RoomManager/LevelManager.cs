using UnityEngine;
using UnityEngine.SceneManagement;

/**
* Handle the room loading and cutscenes etc.
*/
public class LevelManager : MonoBehaviour {

	/**
	* Shortcuts For Loading Scenes. 
	*/
	void Update () {
//		if (Input.GetKeyUp (KeyCode.Alpha1)) {
//			SceneManager.LoadScene ("room_1");
//		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
//			SceneManager.LoadScene ("room_2");
//		}
	}

	/**
	* Get current index add one
	*/
	static public void LoadNextRoom() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1, LoadSceneMode.Single);
	}

	static public void LoadStartScreen() {
		SceneManager.LoadScene ("startMenu", LoadSceneMode.Single);
	}

	static public void LoadRoom (string roomName){
		SceneManager.LoadScene (roomName, LoadSceneMode.Single);
	}

	/**
	* Returns the current Level Number. 
	*/
	static public int getCurrentLevelNumber() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		return levelNumber;
	}
}