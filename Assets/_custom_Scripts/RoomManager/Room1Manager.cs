using UnityEngine;
using System.Collections;

public class Room1Manager : MonoBehaviour {

	private GameObject plate_v, plate_h, crystal_small, crystal_large;
	private GameObject light_plate_v, light_plate_h;
	private int correct_angle_crystal_large, correct_angle_crystal_small, correct_angle_plate_v, correct_angle_plate_h;

	void Start () {
		saveInitialPuzzleState ();
	}

	void LateUpdate () {
		reEvaluatePuzzle ();
	}

	void saveInitialPuzzleState () {
		plate_v = GameObject.Find ("plate_grp_v");
		plate_h = GameObject.Find ("gold_plate_h");
		crystal_small = GameObject.Find ("SM_Cristall_15");
		crystal_large = GameObject.Find ("SM_Cristall_39");
		light_plate_v = plate_v.transform.FindChild ("lichtstrahl").gameObject;
		light_plate_h = plate_h.transform.FindChild ("lichtstrahl").gameObject;

		// save correct angle first
		correct_angle_crystal_large = (int)crystal_large.transform.rotation.eulerAngles.y;
		correct_angle_crystal_small = (int)crystal_small.transform.rotation.eulerAngles.y;
		correct_angle_plate_v = (int)plate_v.transform.rotation.eulerAngles.x;
		correct_angle_plate_h = (int)plate_h.transform.rotation.eulerAngles.y;

		// change starting position
		plate_v.transform.Rotate (new Vector3(-21,0,0)); // x -21 to 14 step 7
		plate_h.transform.Rotate (new Vector3(0,60,0)); // y -80 to 60 step 20
		crystal_small.transform.Rotate (new Vector3(0,60,0)); // step 30
		crystal_large.transform.Rotate (new Vector3(0,90,0)); // step 45

		// disable beam of light
		light_plate_v.SetActive (false);
		light_plate_h.SetActive (false);
		foreach (Transform light in crystal_large.transform) {
			light.gameObject.SetActive (false);
		}
	}

	void reEvaluatePuzzle () {
		
		bool activate_large_crystal = correctAngle (crystal_small);
		bool activate_v = (activate_large_crystal && correctAngle (crystal_large));
		bool activate_h = ((activate_large_crystal && correctAngle (crystal_large)) || equalAngleY (crystal_small, 270));
		bool activate_book = (activate_v && activate_h && correctAngle (plate_v) && correctAngle (plate_h));

		light_plate_v.SetActive (activate_v);
		light_plate_h.SetActive (activate_h);
		foreach (Transform light in crystal_large.transform) {
			light.gameObject.SetActive (activate_large_crystal);
		}

		if (activate_book) {
			// will be called alot once finished
			Debug.Log ("You solved it");
		}
	}

	bool equalAngleY(GameObject go, int angle) {
		return (int)go.transform.rotation.eulerAngles.y == angle;
	}
	bool correctAngle(GameObject go) {
		Vector3 r = go.transform.rotation.eulerAngles;
		if (go == crystal_small) return (int)r.y == correct_angle_crystal_small;
		if (go == crystal_large) return (int)r.y == correct_angle_crystal_large;
		if (go == plate_v) return (int)r.x == correct_angle_plate_v;
		if (go == plate_h) return (int)r.y == correct_angle_plate_h;
		return false;
	}
}
