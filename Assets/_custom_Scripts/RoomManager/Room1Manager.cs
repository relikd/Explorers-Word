using UnityEngine;
using System.Collections;

public class Room1Manager : MonoBehaviour {

	private Transform plate_v, plate_h, crystal_small, crystal_large;
	private int correct_angle_plate_v, correct_angle_plate_h;
	private string lastPuzzleState;
	private ArrayList activeLights;

	private const string LIGHT_IDENT = "lichtstrahl";

	void Start () {
		saveInitialPuzzleState ();
		activeLights = new ArrayList ();
	}

	void LateUpdate () {
		reEvaluatePuzzle ();
	}

	void saveInitialPuzzleState () {
		plate_v = GameObject.Find ("plate_grp_v").transform;
		plate_h = GameObject.Find ("gold_plate_h").transform;
		crystal_small = GameObject.Find ("SM_Cristall_15").transform;
		crystal_large = GameObject.Find ("SM_Cristall_39").transform;

		// save correct angle first
		correct_angle_plate_v = (int)plate_v.rotation.eulerAngles.x;
		correct_angle_plate_h = (int)plate_h.rotation.eulerAngles.y;

		// change starting position
		plate_v.Rotate (new Vector3(-21,0,0)); // x -21 to 14 step 7
		plate_h.Rotate (new Vector3(0,60,0)); // y -80 to 60 step 20
		crystal_small.Rotate (new Vector3(0,60,0)); // step 30
		crystal_large.Rotate (new Vector3(0,90,0)); // step 45

		// disable initial beams of light
		plate_v.FindChild (LIGHT_IDENT).gameObject.SetActive (false);
		plate_h.FindChild (LIGHT_IDENT).gameObject.SetActive (false);
		foreach (Transform light in crystal_large) {
			light.gameObject.SetActive (false);
		}
	}

	void reEvaluatePuzzle () {

		if (currentPuzzleState ().Equals (lastPuzzleState))
			return;
		lastPuzzleState = currentPuzzleState ();


		// clear previous
		foreach (Transform light in activeLights) {
			light.gameObject.SetActive (false);
		}
		activeLights.Clear ();

		// begin all ray casting from first small crystal
		lightHitObject (crystal_small);


		// check if light is on (in activeLights) and angle is correct
		if (correctAngle (plate_v) && correctAngle (plate_h) && 
			activeLights.Contains (plate_v.FindChild (LIGHT_IDENT)) &&
			activeLights.Contains (plate_h.FindChild (LIGHT_IDENT)))
		{
			// activate book
			Debug.Log ("You solved it");
		}
	}

	string currentPuzzleState() {
		string state = "s:" + crystal_small.rotation.eulerAngles.y;
		state += ",l:" + crystal_large.rotation.eulerAngles.y;
		state += ",v:" + plate_v.rotation.eulerAngles.x;
		state += ",h:"+plate_h.rotation.eulerAngles.y;
		return state;
	}

	void lightHitObject(Transform t) {
		// go through all children
		for (int i = 0; i < t.childCount; i++) {
			Transform child = t.GetChild (i);
			lightHitObject (child);
		}
		// if light beam found recast
		if (t.name.Contains (LIGHT_IDENT) && !activeLights.Contains (t))
		{
			activeLights.Add (t);
			t.gameObject.SetActive (true);
			// set length of light beam
			RaycastHit hit;
			Ray ray = new Ray (t.position, t.up);
			if (Physics.Raycast (ray, out hit, 100)) {
				// TODO: adjust distance for specific objects
				t.localScale = new Vector3 (1, hit.distance, 1);
				lightHitObject (hit.transform);
			} else {
				t.localScale = new Vector3 (1, 100, 1);
			}
		}
	}

	bool correctAngle(Transform go) {
		Vector3 r = go.rotation.eulerAngles;
		if (go == plate_v) return (int)r.x == correct_angle_plate_v;
		if (go == plate_h) return (int)r.y == correct_angle_plate_h;
		return false;
	}
}
