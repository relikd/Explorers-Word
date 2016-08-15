using UnityEngine;
using System.Collections;

public class Room1Manager : MonoBehaviour {

	public static bool puzzleSolved = false;

	private GameObject globe;
	private Transform plate_v, plate_h, crystal_small, crystal_large;
	private int correct_angle_plate_v, correct_angle_plate_h;
	private string lastPuzzleState;
	private ArrayList activeLights;

	private const string LIGHT_IDENT = "lichtstrahl";

	void Start () {
		plate_v = GameObject.Find ("plate_grp_v").transform;
		plate_h = GameObject.Find ("gold_plate_h").transform;
		crystal_small = GameObject.Find ("crystal_small").transform;
		crystal_large = GameObject.Find ("crystal_large").transform;
		globe = GameObject.Find ("globe");

		activeLights = new ArrayList ();

		saveInitialPuzzleState ();

		startBackgroundMusic ();
	}

	void Update () {
//		if (Input.GetKeyUp (KeyCode.Alpha5)) {
//			crystal_small.eulerAngles = new Vector3 (0,270,0);
//		}
		reEvaluatePuzzle ();
	}

	void saveInitialPuzzleState () {
		// save correct angle first
		correct_angle_plate_v = (int)plate_v.rotation.eulerAngles.x;
		correct_angle_plate_h = (int)plate_h.rotation.eulerAngles.y;

		// change starting position
		plate_v.Rotate (new Vector3(7,0,0)); // x -21 to 14 step 7
		plate_h.Rotate (new Vector3(0,60,0)); // y -40 to 60 step 20
		crystal_small.Rotate (new Vector3(0,60,0)); // step 30
		crystal_large.Rotate (new Vector3(0,90,0)); // step 45

		// initially disable all lightbeams
		foreach(GameObject light in GameObject.FindGameObjectsWithTag("lightbeam")) {
			light.SetActive (false);
		}
	}

	void reEvaluatePuzzle () {

		if (puzzleSolved)
			activateMayaBook (true); // will be called continuously for glow effect

		if (currentPuzzleState ().Equals (lastPuzzleState))
			return;
		lastPuzzleState = currentPuzzleState ();


		// clear previous
		foreach (Transform light in activeLights) {
			light.gameObject.SetActive (false);
		}
		activeLights.Clear ();
		activateGlobe (false);

		// begin all ray casting from first small crystal
		lightHitObject (crystal_small);


		// check if light is on (in activeLights) and angle is correct
		if (correctAngle (plate_v) && correctAngle (plate_h) &&
		    activeLights.Contains (plate_v.FindChild (LIGHT_IDENT)) &&
		    activeLights.Contains (plate_h.FindChild (LIGHT_IDENT))) {
			// activate book
			Debug.Log ("You solved it");
			puzzleSolved = true;
		} else {
			puzzleSolved = false;
			activateMayaBook (false);
		}
	}

	void activateMayaBook (bool flag) {
		Color finalColor = Color.black;
		if (flag) {
			float emission = 0.01f + Mathf.PingPong (Time.time, 0.7f)/2.0f;
			Color baseColor = Color.yellow;
			finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		}

		GameObject maya_book = GameObject.Find ("maya_book");
		Renderer renderer =	maya_book.GetComponent<Renderer> ();
		Material mat = renderer.material;
		mat.SetColor ("_EmissionColor", finalColor);

		TriggerInteraction script = maya_book.GetComponent<TriggerInteraction> ();
		script.triggerActive = flag;
	}

	string currentPuzzleState() {
		string state = "s:" + crystal_small.rotation.eulerAngles.y;
		state += ",l:" + crystal_large.rotation.eulerAngles.y;
		state += ",v:" + plate_v.rotation.eulerAngles.x;
		state += ",h:"+plate_h.rotation.eulerAngles.y;
		return state;
	}

	void lightHitObject(Transform t) {
		if (t == globe.transform)
			activateGlobe (true);
		
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
				float dist = hit.distance;

				if (hit.transform == crystal_large) dist += 0.2f;
				else if (hit.transform == plate_v) dist += 0.12f;
				else if (hit.transform == plate_h) dist += 0.13f;
				else if (hit.transform == crystal_small) dist += 0.08f;

				t.localScale = new Vector3 (1, dist, 1);
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

	void activateGlobe(bool flag) {
		Renderer renderer = globe.GetComponent <Renderer>();
		Color emissisonColor = ( flag ? Color.white : Color.black );
		if (flag) emissisonColor.a = 0.5f;
		renderer.material.SetColor ("_EmissionColor", emissisonColor);
	}

	void startBackgroundMusic(){
		SceneSound2D sceneSoundManager = gameObject.GetComponent<SceneSound2D> ();
		Debug.Log (sceneSoundManager);
		sceneSoundManager.startSound (0);
		sceneSoundManager.toggleLoop (0);
	}
}
