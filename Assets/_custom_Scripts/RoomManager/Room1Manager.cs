using UnityEngine;

/**
 * Evalutate level state for Room 1.
 * All lightbeams have to be rotated to the correct position
 */
public class Room1Manager : MonoBehaviour {

	[SerializeField] private GameObject startingLightbeam;
	[SerializeField] private GameObject finishCrystal;
	[SerializeField] private int lightsRequiredToFinish = 3;
	[SerializeField] private GameObject[] rotatableObjects;

	private GlobalSoundPlayer m_globalSoundPlayer;

	/** Puzzle is solved if the right amount of light hits the final crystal */
	private int currentLightsOnFinish = 0;
	/** Flag used to recast lightbeams (saves calculation time) */
	private bool shouldRecalculate = false;
	[SerializeField] private GameObject mayaBook;
	[SerializeField] private GameObject globe;

	// only used to get an redraw message from after a text input
	void OnEnable () { Events.instance.AddListener<XplrEvents.WordEntered>(OnNewWordEntered); }
	void OnDisable () { Events.instance.RemoveListener<XplrEvents.WordEntered>(OnNewWordEntered); }
	/** Called every time user enters something in the Explorer's Book */
	void OnNewWordEntered (XplrEvents.WordEntered e) {
		if (e.exists)
			shouldRecalculate = true;
	}

	void Start () {
		m_globalSoundPlayer = gameObject.GetComponent<GlobalSoundPlayer> ();
		initialPuzzleTwist ();
		m_globalSoundPlayer.StartAudio ();
		m_globalSoundPlayer.PlayOtherSceneSound (0, true, 1.0f, true);
	}
	/**
	 * Will rotate the initial correct state to a wrong one. So the puzzle isn't solved immediatelly
	 */
	void initialPuzzleTwist () {
		foreach (GameObject item in rotatableObjects) {
			Interaction.RotateInteraction ri_script = item.GetComponent<Interaction.RotateInteraction> ();
			if (ri_script)
				item.transform.Rotate (ri_script.rotateBy*2);
		}
	}
	/**
	 * If game state changes it will recast all lightbeams, otherwise only maya book animation
	 */
	void Update () {
		if (currentLightsOnFinish >= lightsRequiredToFinish)
			activateMayaBook (true); // will be called continuously for glow effect

		foreach (GameObject item in rotatableObjects)
			shouldRecalculate |= item.transform.hasChanged;

		if (shouldRecalculate) {
			shouldRecalculate = false;
			foreach (GameObject item in rotatableObjects)
				item.transform.hasChanged = false;
			
			clearAllLights ();
			lightHitObject (startingLightbeam.transform);
		}
	}
	/**
	 * Deactivates all lightbeams and set solved state to false
	 */
	void clearAllLights() {
		// initially disable all lightbeams
		GameObject[] lights = GameObject.FindGameObjectsWithTag("lightbeam");
		foreach(GameObject light in lights)
			light.SetActive (false);
		currentLightsOnFinish = 0;
		activateGlobe (false);
		activateMayaBook (false);
	}
	/**
	 * Will recursively cast lightbeams for all children of target object
	 */
	void lightHitObject(Transform t) {
		// check for special objects
		if (t == null)
			return;
		else if (t == finishCrystal.transform)
			currentLightsOnFinish++;
		else if (t == globe.transform)
			activateGlobe (true);
		
		// go recursively through all children
		for (int i = 0; i < t.childCount; i++) {
			Transform child = t.GetChild (i);
			lightHitObject (child);
		}
		// if light beam then recast
		LightbeamExpansion lbe_script = t.GetComponent<LightbeamExpansion> ();
		if (lbe_script && !t.gameObject.activeSelf) {
			t.gameObject.SetActive (true);
			lightHitObject (lbe_script.Expand ());
		}
	}
	/**
	 * Activates the easter egg
	 */
	void activateGlobe(bool flag) {
		Renderer renderer = globe.GetComponent <Renderer>();
		Color emissisonColor = ( flag ? Color.white : Color.black );
		if (flag) emissisonColor.a = 0.5f;
		renderer.material.SetColor ("_EmissionColor", emissisonColor);
	}
	/**
	 * Will activate the book glow and trigger interaction
	 */
	void activateMayaBook (bool flag) {
		Color finalColor = Color.black;
		if (flag) {
			float emission = 0.01f + Mathf.PingPong (Time.time, 0.7f) / 2.0f;
			Color baseColor = Color.yellow;
			finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		}

		Renderer renderer =	mayaBook.GetComponent<Renderer> ();
		Material mat = renderer.material;
		mat.SetColor ("_EmissionColor", finalColor);

		Interaction.TriggerInteraction script = mayaBook.GetComponent<Interaction.TriggerInteraction> ();
		script.triggerActive = flag;
	}
}
