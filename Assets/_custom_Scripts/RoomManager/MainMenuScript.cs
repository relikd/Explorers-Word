using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RoomManager
{
	/**
	* Manages the Main Menu. For Example Button Interactions to start the Game. 
	*/
	public class MainMenuScript : MonoBehaviour {
	
		public Button startGameButton;
		public Button tutorialButton;
		public Button exitGameButton;
		public Button submitExitGame;
		public Button cancelExitGame;
		public Canvas quitMenuPopUp;

		private int selectedButtonIndex = -1;

		private GlobalSoundPlayer globalSoundPlayer;
		// Use this for initialization
		/**
		* Initialises nesseccary Components
		*/
		void Start () {
			lockCursor ();
			startGameButton = startGameButton.GetComponent<Button>();
			tutorialButton = tutorialButton.GetComponent<Button>();
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
				//selectedButton = startGameButton;
				//selectedButton.Select ();
				if(selectedButtonIndex <= 0) 
					selectedButtonIndex = 0;
				else 
					selectedButtonIndex--;
			}
			if (Input.GetKeyUp(KeyCode.DownArrow)) {
				//selectedButton = exitGameButton;
				//selectedButton.Select ();
				if(selectedButtonIndex > 2) 
					selectedButtonIndex = 2;
				else 
					selectedButtonIndex++;
			}

			switch(selectedButtonIndex){
			case 0:
				{
					selectedButton = startGameButton;
					selectedButton.Select ();
				}break;
			case 1:
				{
					selectedButton = tutorialButton;
					selectedButton.Select ();
				}break;
			case 2:
				{
					selectedButton = exitGameButton;
					selectedButton.Select ();
				}break;
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
		* Unlocks the cursor and makes it invisible
		*/
		void lockCursor() {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		/**
		* Starts Coroutine for Level Change
		*/
		public void StartGame(){
			StartCoroutine (FadeAndChangeLevel());
			Cursor.visible = false;
		}
		public void StartTutorial(){
			LevelManager.LoadTutorial ();
			Cursor.visible = false;
		}
		/**
		* Fades Out and Starts first Level
		*/
		IEnumerator FadeAndChangeLevel(){
			float fadeTime = gameObject.GetComponent<SceneFadingScript> ().BeginFadeOut();
			yield return new WaitForSeconds (fadeTime);
			LevelManager.LoadRoom (0);
		}
		/**
		* Canceles the Exit Game Menu
		*/
		public void ExitGameCanceledPressed(){
			quitMenuPopUp.enabled = false;
			startGameButton.enabled = true;
			exitGameButton.enabled = true;
		}
		/**
		* Shows the Exit Game Menu
		*/
		public void ExitGamePressed(){
			quitMenuPopUp.enabled = true;
			startGameButton.enabled = false;
			exitGameButton.enabled = false;
		}
		/**
		* Quit the Game
		*/
		public void ExitGame(){
			Application.Quit ();
		}
	}
}