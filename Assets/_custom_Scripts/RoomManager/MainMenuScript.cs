using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/**
* Manages the Main Menu. For Example Button Interactions to start the Game. 
*/
public class MainMenuScript : MonoBehaviour {

	public Button startGameButton;
	public Button exitGameButton;
	public Button submitExitGame;
	public Button cancelExitGame;
	public Canvas quitMenuPopUp;	


	private GlobalSoundPlayer globalSoundPlayer;
	// Use this for initialization
	/**
	* Initialises nesseccary Components. 
	*/
	void Start () {
		startGameButton = startGameButton.GetComponent<Button>();
		exitGameButton = exitGameButton.GetComponent < Button> ();
		quitMenuPopUp = quitMenuPopUp.GetComponent<Canvas> ();
		quitMenuPopUp.enabled = false;
		globalSoundPlayer = this.gameObject.GetComponent<GlobalSoundPlayer> ();
		globalSoundPlayer.StartAudio ();
	}


	/**
	* Selects the Keys for User Input . . . 
	*/
	void Update(){
		Button selectedButton = null;
		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			selectedButton = startGameButton;
			selectedButton.Select ();
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			selectedButton = exitGameButton;
			selectedButton.Select ();
		}

		if (quitMenuPopUp.enabled == true) {
			if (Input.GetKeyUp(KeyCode.LeftArrow)) {
				selectedButton = submitExitGame;
				selectedButton.Select ();
			}
			if (Input.GetKeyUp(KeyCode.RightArrow)) {
				selectedButton = cancelExitGame;
				selectedButton.Select ();
			}
		}
	}

	/**
	* Starts Coroutine for Level Change. 
	*/
	public void StartGame(){
		StartCoroutine (FadeAndChangeLevel());
	}

	/**
	* Fades Out and Starts Level. 
	*/
	IEnumerator FadeAndChangeLevel(){
		float fadeTime = gameObject.GetComponent<SceneFadingScript> ().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		LevelManager.LoadRoom ("room_0");
	}

	/**
	* Canceles the Exit Game Menu. 
	*/
	public void ExitGameCanceledPressed(){
		quitMenuPopUp.enabled = false;
		startGameButton.enabled = true;
		exitGameButton.enabled = true;
	}

	/**
	* Shows the Exit Game Menu. 
	*/
	public void ExitGamePressed(){
		quitMenuPopUp.enabled = true;
		startGameButton.enabled = false;
		exitGameButton.enabled = false;
	}

	/**
	* Quit the Game. 
	*/
	public void ExitGame(){
		Application.Quit ();
	}

}
