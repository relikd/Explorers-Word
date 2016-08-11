using UnityEngine;
using System.Collections;

public class Room1Manager : MonoBehaviour {
	
	void Start () {
		GameObject v = GameObject.Find ("plate_grp_v");
		GameObject h = GameObject.Find ("gold_plate_h");

		// change starting position
		v.transform.Rotate (new Vector3(-21,0,0)); // x -21 to 14 step 7
		h.transform.Rotate (new Vector3(0,60,0)); // y -80 to 60 step 20
	}
	
	void Update () {
		
	}
}
