
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnLightOn : MonoBehaviour
{
	// INSPECTOR SETTINGS
	[Header("Detection Light Settings")]
	//[Tooltip("Within this radius the player is able to open/close the door")]
	public float Reach = 4F;
	//[Tooltip("The tag that triggers the object to be openable")]
	public string TriggerTag = "VictorianLights";

	// PRIVATE SETTINGS
	[HideInInspector] public bool InReach;

	//DEBUGGING (DEBUG PANEL)
	[Header("Debug Settings")]
	string CategoryDetection = "Detection";
	string TitleReach = "Reach";
	string TitleInReach = "InReach";

	string CategoryVictorianLights = "VictorianLights";
	string TitleHitTag = "HitTag";
	string TitleCurrentAngle = "CurrentAngle";
	string TitleSpeed = "Speed";
	string TitleTimesMoveable = "TimesMoveable";
	string TitleRunning = "ActionRunning";

	//START FUNCTION
	void Start()
	{
		enabled = true;
	}

	//UPDATE FUNCTION
	void Update()
	{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));

		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach))
		{
			//DEBUGGING (DEBUG PANEL)
			DebugPanel.Log(TitleHitTag, CategoryVictorianLights, hit.collider.tag);

			if(hit.collider.tag == TriggerTag)
			{
				InReach = true;

				if (Input.GetKeyUp(KeyCode.E))
				{
					// Give the object that was hit the name 'Door'.
					GameObject CurrentVictorianLight = hit.transform.gameObject;

					// Get access to the 'DoorOpening' script attached to the door that was hit.
					VictorianLight dooropening = CurrentVictorianLight.GetComponent<VictorianLight>();

					// Check whether the door is opening/closing or not.
					if (dooropening.Running == false)
					{
						// Open/close the door by running the 'Open' function in the 'DoorOpening' script.
						hit.collider.GetComponent<VictorianLight>().TurnLightsOn();
					}
				}
			}

			else InReach = false;

		}

		else
		{
			InReach = false;

			//DEBUGGING (DEBUG PANEL)
			DebugPanel.Break(TitleHitTag);
		}

		//DEBUGGING (DEBUG PANEL)
		DebugPanel.Log(TitleInReach, CategoryDetection, InReach);
		DebugPanel.Log(TitleReach, CategoryDetection, Reach);

		if (InReach == true) {
			DebugPanel.Log (TitleRunning, CategoryVictorianLights, hit.collider.GetComponent<VictorianLight> ().Running);
		}

		else
		{
			DebugPanel.Break (TitleRunning);
		}

		// Draw the ray as a colored line for debugging purposes.
		//Debug.DrawRay (ray.origin, ray.direction*Reach, DebugRayColor);
	}
}
