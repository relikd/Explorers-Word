using UnityEngine;
using System.Collections;
using Interaction;

public class DeactivateGO : MonoBehaviour, Interactable {

	public bool shouldPickUpStatic = false;
	private bool shouldDepictText;

	void Start ()
	{
		
	}
		
	void LateUpdate() {

	}

	public void HandleRaycastCollission() {
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();

		gameObject.isStatic = !shouldPickUpStatic;

		if (Input.GetKeyUp (KeyCode.T) ) {		 
			this.gameObject.SetActive(false);
		}
	}
	
	public void EnableGUI(bool enable) {
		shouldDepictText = enable;
	}

	void OnGUI ()
	{
		if (shouldDepictText)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to take");
		}
	}
}