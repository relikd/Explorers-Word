using UnityEngine;
using System.Collections;

public class CreditsTranslation : MonoBehaviour {

	[SerializeField][Range(0.2f,1.0f)]public float creditRunSpeed;
	private bool runTranslation = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (runTranslation) {
			gameObject.transform.Translate(new Vector3(0,creditRunSpeed,0));
		}
	}

	public void StartCredits(){
		runTranslation = true;
	}
}
