using UnityEngine;
using UnityEngine.SceneManagement;

/** Handle room and cutscene loading */
public class LevelManager
{
	/** Used for level loading (0 = room 0, etc.) */
	public static int currentLevel = 0;

	/** Load level based on #currentLevel */
	static public void LoadNextRoom() {
		SceneManager.LoadScene (currentLevel + 3, LoadSceneMode.Single); // current build settings
	}
	/** Load main menu */
	static public void LoadStartScreen() {
		SceneManager.LoadScene ("startMenu", LoadSceneMode.Single);
	}
	/** Load tutorial room */
	static public void LoadTutorial() {
		SceneManager.LoadScene ("room_Tutorial", LoadSceneMode.Single);
	}
	/** Load cutscene and increase #currentLevel */
	static public void LeaveRoom (){
//		currentLevel++;
		currentLevel = SceneManager.GetActiveScene ().buildIndex -3 +1; // for development
		SceneManager.LoadScene ("cutscene", LoadSceneMode.Single);
	}
}