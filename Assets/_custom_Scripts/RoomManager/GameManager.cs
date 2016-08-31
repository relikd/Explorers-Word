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
	private XplrCharacter.FPSController FPSScript;

	/**
	* Instantiates the neccessary class variables. 
	*/
	void Awake() {
		FPSScript = FPS.GetComponent<XplrCharacter.FPSController> ();
	}

	/**
	* Disables Walking in Custom First Person Controller Script. 
	*/
	public void disableWalking(bool flag) {
		FPSScript.shouldWalk = !flag;
		FPSScript.m_UseHeadBob = !flag;
	}

	/**
	* Disables Jumping in Custom First Person Controller Script. 
	*/
	public void disableJumping(bool flag) {
		FPSScript.shouldJump = !flag;
	}

	/**
	* Disables Audio in Custom First Person Controller Script. 
	*/
	public void disablePlayerAudioSource(bool flag) {
		FPSScript.shouldPlayAudioSounds = !flag;
	}

	/**
	* Disables the Camera in Custom First Person Controller Script. 
	*/
	public void disableCammeraRotation(bool flag) {
		FPSScript.shouldLookAround = !flag;
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

