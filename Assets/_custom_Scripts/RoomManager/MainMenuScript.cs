using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Button startGameButton;
	public Button exitGameButton;
	public Canvas quitMenuPopUp;	


	private GlobalSoundPlayer globalSoundPlayer;
	// Use this for initialization
	void Start () {
		startGameButton = startGameButton.GetComponent<Button>();
		exitGameButton = exitGameButton.GetComponent < Button> ();
		quitMenuPopUp = quitMenuPopUp.GetComponent<Canvas> ();
		quitMenuPopUp.enabled = false;
		globalSoundPlayer = this.gameObject.GetComponent<GlobalSoundPlayer> ();
		globalSoundPlayer.StartAudio ();
	}

	public void StartGame(){
		SceneManager.LoadScene ("room_0", LoadSceneMode.Single);
	}

	public void ExitGameCanceledPressed(){
		quitMenuPopUp.enabled = false;
		startGameButton.enabled = true;
		exitGameButton.enabled = true;
	}

	public void ExitGamePressed(){
		quitMenuPopUp.enabled = true;
		startGameButton.enabled = false;
		exitGameButton.enabled = false;
	}

	public void ExitGame(){
		Application.Quit ();
	}

}
