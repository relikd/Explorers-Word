using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
/**
*  Handles the Pause Menu 
*/
public class PauseMenu : MonoBehaviour {
		
	public GameObject pauseMenu;
	private bool shouldShowMenu = false;

	/**
	* Disables the Pause Menu on Start Up
	*/
	void Start() {
		if (pauseMenu) {
			pauseMenu.SetActive (false);
		}
	}
	/**
	* Shows / Disables Pause Menu when Escape is Pressed
	*/
	void LateUpdate(){
		if (Input.GetKeyUp(KeyCode.Escape)) {
			togglePauseMenu();
			lockCursor ();
		}
	}

	/**
	* Toggles the PauseMenu
	*/
	private void togglePauseMenu() {
		shouldShowMenu = !shouldShowMenu;
		pauseMenu.SetActive (shouldShowMenu);
		toggleCrosshair ();
		XplrGUI.BookController.disableBook = shouldShowMenu;
	}

	/**
	* Toggles the Crosshair
	*/
	private void toggleCrosshair() {
		bool canEnableCrosshair = GameManager.getInstance ().canEnableCrosshair ();
		XplrGUI.MouseCrosshair.showCrosshair = (canEnableCrosshair && !shouldShowMenu)?true:false;
	}
	
	/**
	* Unlocks the cursor and makes it invisible. 
	*/
	void lockCursor() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = shouldShowMenu;
	}
				
	/**
	* Resumes to the Game. 
	*/
	public void ResumeGame(){
		togglePauseMenu ();
		lockCursor ();
	}

	/**
	* Quit the Game. 
	*/
	public void ExitGame(){
		LevelManager.LoadStartScreen();
	}

}
