using UnityEngine;
using UnityEngine.SceneManagement;

/** Handle room and cutscene loading */
public class LevelManager
{
	/** Used for level loading (0 = room 0, etc.) */
	public static int currentLevel = 0;

	/** Load level based on #currentLevel */
	static public void LoadRoom(int index) {
		SceneManager.LoadScene (index + 3, LoadSceneMode.Single); // current build settings
	}
	/** Load main menu */
	static public void LoadStartScreen() {
		SceneManager.LoadScene ("StartMenu", LoadSceneMode.Single);
	}
	/** Load tutorial room */
	static public void LoadTutorial() {
		SceneManager.LoadScene ("Tutorial", LoadSceneMode.Single);
	}
	/** Load cutscene and increase #currentLevel */
	static public void LeaveRoom (){
//		currentLevel++;
		// TODO: save unlocked room
		currentLevel = SceneManager.GetActiveScene ().buildIndex -3; // for development
		SceneManager.LoadScene ("Cutscene", LoadSceneMode.Single);
	}
}