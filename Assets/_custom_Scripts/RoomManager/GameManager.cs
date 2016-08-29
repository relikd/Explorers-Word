using System;
using UnityEngine;

/**
* Singleton that provides Features like Disabling the Walking of the Player or to Lock the Player camera. 
*/
public  class GameManager : MonoBehaviour
{
	private static GameManager GameManagerInstance;
	public GameObject Player;
	public GameObject FPC;
	public GameObject FPS;
	private 
	UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSScript;
	MouseCrosshair mouseCrosshair;
	ExplorersBook.OpenExplorersBook explorersBook;

	/**
	* Instantiates the neccessary class variables. 
	*/
	void Awake() {
		FPSScript = FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
		mouseCrosshair = FPS.GetComponent<MouseCrosshair> ();
		explorersBook = FPC.GetComponent<ExplorersBook.OpenExplorersBook> ();
	}

	/**
	* Disables the book. 
	*/
	public  void disableExplorersBook() {		
		ExplorersBook.OpenExplorersBook OpenExplBook = FPC.GetComponent<ExplorersBook.OpenExplorersBook> ();
		OpenExplBook.shouldShowBook = !OpenExplBook.shouldShowBook;
	}

	/**
	* Disables Walking in Custom First Person Controller Script. 
	*/
	public void disableWalking() {
		FPSScript.shouldWalk = !FPSScript.shouldWalk;
		FPSScript.m_UseHeadBob = !FPSScript.m_UseHeadBob;
	}

	/**
	* Disables Right book page of Explorers Book. 
	*/
	public void disableRightBookPage() {
		explorersBook.shouldShowLeftPage = false;
	}

	/**
	* Disables Jumping in Custom First Person Controller Script. 
	*/
	public void disableJumping() {
		FPSScript.shouldJump = !FPSScript.shouldJump;
	}

	/**
	* Disables Audio in Custom First Person Controller Script. 
	*/
	public void disablePlayerAudioSource() {
		FPSScript.shouldPlayAudioSounds = !FPSScript.shouldPlayAudioSounds;
	}

	/**
	* Disables the Camera in Custom First Person Controller Script. 
	*/
	public void disableCammeraRotation() {
		FPSScript.shouldLookAround = !FPSScript.shouldLookAround;
	}

	public void disableCrosshair() {
		if (mouseCrosshair) {		
			mouseCrosshair.enabled = !mouseCrosshair.enabled;
		}
	}

	/**
	* Method for getting the Singleton Instance. 
	*/
	public static GameManager getInstance() {
		if (GameManagerInstance == null) {
			GameManagerInstance = GameObject.Find("LevelManager").GetComponent<GameManager>();
		}
		return GameManagerInstance;
	}
}

