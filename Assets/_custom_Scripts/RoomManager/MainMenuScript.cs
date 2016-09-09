using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RoomManager{
	/// <summary>
	/// Manages the Main Menu. Including button interactions, toggling visibility of the elements and loading the current gamestates
	/// </summary>
	public class MainMenuScript : MonoBehaviour {

		public Button startGameButton;
		public Button openChapterSelectionButton;
		public Button[] chaptersAsButtons;
		public Button tutorialButton;
		public Button exitGameButton;
		public Button submitExitGame;
		public Button cancelExitGame;
		public Canvas chapterSelectionScreen;
		public Canvas quitMenuPopUp;	

		private int selectedButtonIndex = -1;


		private GlobalSoundPlayer globalSoundPlayer;

		/// <summary>
		/// Initializes the neccessary components
		/// </summary>
		void Start () {
			lockCursor ();
			startGameButton = startGameButton.GetComponent<Button>();
			openChapterSelectionButton = openChapterSelectionButton.GetComponent<Button> ();
			tutorialButton = tutorialButton.GetComponent<Button>();
			exitGameButton = exitGameButton.GetComponent < Button> ();
			chapterSelectionScreen = chapterSelectionScreen.GetComponent<Canvas> ();
			quitMenuPopUp = quitMenuPopUp.GetComponent<Canvas> ();

			LoadSaveStates ();

			chapterSelectionScreen.enabled = false;
			quitMenuPopUp.enabled = false;
			globalSoundPlayer = this.gameObject.GetComponent<GlobalSoundPlayer> ();
			globalSoundPlayer.StartAudio ();
		}


		/// <summary>
		/// Handles the Input Keys and toggles the visibility of MainMenuCanvas, QuitPopUp and ChapterSelectionScreen(Canvas)
		/// </summary>
		void Update(){
			if (Input.GetKey (KeyCode.E) && Input.GetKey (KeyCode.N)) {
				SaveAndLoad.LevelsCompleted = 3;
				LoadSaveStates ();
			}
			if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.I)) {
				SaveAndLoad.LevelsCompleted = 0;
				LoadSaveStates ();
			}

			Button selectedButton = null;
			if (!quitMenuPopUp.enabled && !chapterSelectionScreen.enabled) {
				if (Input.GetKeyUp(KeyCode.UpArrow)) {
					if(selectedButtonIndex <= 0) 
						selectedButtonIndex = 0;
					else 
						selectedButtonIndex--;
				}
				if (Input.GetKeyUp(KeyCode.DownArrow)) {
					if(selectedButtonIndex > 3) 
						selectedButtonIndex = 3;
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
						selectedButton = openChapterSelectionButton;
						selectedButton.Select ();
					}break;
				case 2: 
					{
						selectedButton = tutorialButton;
						selectedButton.Select ();
					}break;
				case 3: 
					{
						selectedButton = exitGameButton;
						selectedButton.Select ();
					}break;

				}
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

			if (chapterSelectionScreen.enabled == true) {
				if (Input.GetKeyUp (KeyCode.Escape)) {
					CloseChapterSelectionScreen ();
				}
				if (Input.GetKeyUp(KeyCode.UpArrow)) {
					if(selectedButtonIndex <= 0) 
						selectedButtonIndex = 0;
					else 
						selectedButtonIndex--;
				}
				if (Input.GetKeyUp(KeyCode.DownArrow)) {
					if(selectedButtonIndex > chaptersAsButtons.Length - 1) 
						selectedButtonIndex = chaptersAsButtons.Length - 1;
					else 
						selectedButtonIndex++;

					selectedButton = chaptersAsButtons [selectedButtonIndex];
					selectedButton.Select ();
				}
			}
		}

		/// <summary>
		/// Unlocks the cursor and makes it invisibl
		/// </summary>
		void lockCursor() {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		/// <summary>
		/// Opens the chapter selection screen.
		/// </summary>
		public void OpenChapterSelectionScreen(){
			chapterSelectionScreen.enabled = true;
			//ToggleChaptersAsButtonEnableStates (true);
			quitMenuPopUp.enabled = false;
			startGameButton.enabled = false;
			openChapterSelectionButton.enabled = false;
			tutorialButton.enabled = false;
			exitGameButton.enabled = false;
			this.gameObject.GetComponent<Canvas> ().enabled = false;
			lockCursor ();
		}

		/// <summary>
		/// Closes the chapter selection screen.
		/// </summary>
		public void CloseChapterSelectionScreen(){
			chapterSelectionScreen.enabled = false;
			//ToggleChaptersAsButtonEnableStates (false);
			quitMenuPopUp.enabled = false;
			startGameButton.enabled = true;
			openChapterSelectionButton.enabled = true;
			tutorialButton.enabled = true;
			exitGameButton.enabled = true;
			this.gameObject.GetComponent<Canvas> ().enabled = true;
		}

		/// <summary>
		/// Starts Coroutine for Level Change. 
		/// </summary>
		public void StartGame(){
			StartCoroutine (FadeAndChangeLevel(0));
			Cursor.visible = false;
		}

		/// <summary>
		/// Starts a specific chapter depending on the button which is calling this method.
		/// </summary>
		/// <param name="sender">The Button which is calling this method</param>
		public void StartSpecificChapter(Button sender){
			if (sender == chaptersAsButtons [0]) {
				StartCoroutine (FadeAndChangeLevel(1));
			}
			if (sender == chaptersAsButtons [1]) {
				StartCoroutine (FadeAndChangeLevel(2));
			}
			if (sender == chaptersAsButtons [2]) {
				StartCoroutine (FadeAndChangeLevel(3));
			} 

			//TODO:Uncomment when room4 is added to Game + add chapter4button to buttonarray + set chapter4button active in editor
			//		if (sender == chaptersAsButtons [3]) {
			//			StartCoroutine (FadeAndChangeLevel(4));
			//		}
		}

		/// <summary>
		/// Starts the tutorial.
		/// </summary>
		public void StartTutorial(){
			LevelManager.LoadTutorial();
			Cursor.visible = false;
		}

		/// <summary>
		/// Fades Out and Starts Level.
		/// </summary>
		/// <param name="levelname">The name of the level you want to be loaded.</param>
		IEnumerator FadeAndChangeLevel(int roomNumber){
			float fadeTime = gameObject.GetComponent<SceneFadingScript> ().BeginFadeOut();
			yield return new WaitForSeconds (fadeTime);
			LevelManager.LoadRoom (roomNumber);
		}

		/// <summary>
		/// Closes the ExitGame dialog
		/// </summary>
		public void ExitGameCanceledPressed(){
			quitMenuPopUp.enabled = false;
			startGameButton.enabled = true;
			openChapterSelectionButton.enabled = true;
			tutorialButton.enabled = true;
			exitGameButton.enabled = true;
		}

		/// <summary>
		/// Opens the ExitGame dialog
		/// </summary>
		public void ExitGamePressed(){
			quitMenuPopUp.enabled = true;
			startGameButton.enabled = false;
			openChapterSelectionButton.enabled = false;
			tutorialButton.enabled = false;
			exitGameButton.enabled = false;
		}

		/// <summary>
		/// Quits the game
		/// </summary>
		public void ExitGame(){
			Application.Quit ();
		}


		/// <summary>
		/// Loads the save states. (currently only the enabled levels)
		/// </summary>
		private void LoadSaveStates(){
			int highestEnabledLevel = SaveAndLoad.LevelsCompleted;

			int currentButton = 1;
			for (int i = 0; i <= chaptersAsButtons.Length - 1; i++) {
				if (currentButton <= highestEnabledLevel) {
					chaptersAsButtons [i].GetComponent<Text> ().color = Color.white;
					chaptersAsButtons [i].enabled = true;
				} else {
					chaptersAsButtons [i].GetComponent<Text> ().color = Color.gray;
					chaptersAsButtons [i].enabled = false;
				}
				currentButton++;
			}
		}

		public void OpenFeedbackDocumentInWebBrowser(){
			Application.OpenURL ("https://goo.gl/forms/Wg4TjY1n9jlVi5Dj2");
		}
	}
}
