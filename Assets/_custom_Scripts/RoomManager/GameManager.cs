using System;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
	private static GameManager GameManagerInstance;
	public GameObject Player;
	public GameObject FPC;
	public GameObject FPS;
	private 
	UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSScript;

	void Awake() {
		FPSScript = FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
	}

	public  void disableExplorersBook() {		
		ExplorersBook.OpenExplorersBook OpenExplBook = FPC.GetComponent<ExplorersBook.OpenExplorersBook> ();
		OpenExplBook.shouldShowBook = !OpenExplBook.shouldShowBook;
	}

	public void disableWalking() {
		FPSScript.shouldWalk = !FPSScript.shouldWalk;
		FPSScript.m_UseHeadBob = !FPSScript.m_UseHeadBob;
	}

	public void disableJumping() {
		FPSScript.shouldJump = !FPSScript.shouldJump;
	}

	public void disablePlayerAudioSource() {
		FPSScript.shouldPlayAudioSounds = !FPSScript.shouldPlayAudioSounds;
	}

	public void disableCammeraRotation() {
		FPSScript.shouldLookAround = !FPSScript.shouldLookAround;
	}

	public static GameManager getInstance() {
		if (GameManagerInstance == null) {
			GameManagerInstance = GameObject.Find("LevelManager").GetComponent<GameManager>();
		}
		return GameManagerInstance;
	}
}

