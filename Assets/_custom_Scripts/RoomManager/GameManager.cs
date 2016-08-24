using System;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
	private static GameManager GameManagerInstance;
	public GameObject Player;
	public GameObject FPC;
	public GameObject FPS;


	public  void disableExplorersBook() {		
		ExplorersBook.OpenExplorersBook OpenExplBook = FPC.GetComponent<ExplorersBook.OpenExplorersBook> ();
		OpenExplBook.shouldShowBook = !OpenExplBook.shouldShowBook;
	}

	public void disableWalking() {
		UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSScript = FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
		FPSScript.shouldWalk = !FPSScript.shouldWalk;
	}

	public void disableJumping() {
		UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController FPSScript = FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController> ();
		FPSScript.shouldJump = !FPSScript.shouldJump;
	}

	public static GameManager getInstance() {
		if (GameManagerInstance == null) {
			GameManagerInstance = GameObject.Find("LevelManager").GetComponent<GameManager>();
		}
		return GameManagerInstance;
	}
}

