
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnLightsOn : MonoBehaviour
{
	//START FUNCTION
	void Start()
	{

	}

	//UPDATE FUNCTION
	void Update()
	{
		GameObject Player = GameObject.Find("FirstPersonCharacter");
		Reachable detection = Player.GetComponent<Reachable>();

		if (detection.InReach == true)
		{
			if (Input.GetKeyUp (KeyCode.E)) {
				// Give the object that was hit the name 'Door'.
				GameObject CurrentVictorianLight = detection.RaycastHit.transform.gameObject;

				// Get access to the 'DoorOpening' script attached to the door that was hit.
				VictorianLight dooropening = CurrentVictorianLight.GetComponent<VictorianLight> ();

				// Check whether the door is opening/closing or not.
				if (dooropening.Running == false) {
					// Open/close the door by running the 'Open' function in the 'DoorOpening' script.
					detection.RaycastHit.collider.GetComponent<VictorianLight> ().TurnLightsOn ();
				}
			}
		}


	}
}