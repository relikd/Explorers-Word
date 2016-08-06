using UnityEngine;
using System.Collections;

public class DeactivateGO : MonoBehaviour {

	public string TriggerTag;
	public bool shouldPickUpStatic = false;

	void Start ()
	{
		
	}
		
	void LateUpdate() {
	
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();

		gameObject.isStatic = !shouldPickUpStatic;

		if (Input.GetKeyUp (KeyCode.E) ) {
			if (detection.RaycastHit.collider.tag == TriggerTag && detection.InReach == true) {
				this.gameObject.SetActive(false);
			}
		}
	}

	void OnGUI ()
	{
		// Access InReach variable from raycasting script.
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();

		if (detection.InReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to take");
		}
	}
}