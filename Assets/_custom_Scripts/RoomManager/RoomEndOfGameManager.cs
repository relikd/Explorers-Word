using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RoomEndOfGameManager : MonoBehaviour {

	public Canvas creditCanvas;
	public Camera mainCamera;
	public Transform whitePortalBorder;
	public Transform blackPortalBorder;
	private CameraShakeScript cameraShaker;
	private Transform textObject;
	private Text creditText;


	// Use this for initialization
	void Start () {
		mainCamera = mainCamera.GetComponent<Camera> ();
		cameraShaker = mainCamera.GetComponent<CameraShakeScript> ();
		whitePortalBorder = whitePortalBorder.GetComponent<Transform> ();
		blackPortalBorder = blackPortalBorder.GetComponent<Transform> ();
		creditCanvas.enabled = false;
	}


	void Update(){
		float distanceWhite = Vector3.Distance (mainCamera.transform.position, whitePortalBorder.transform.position);
		float distanceBlack = Vector3.Distance (mainCamera.transform.position, blackPortalBorder.transform.position);

		Debug.Log ("Distance white: " + distanceWhite.ToString () + " / Distance black: " + distanceBlack.ToString ());
		if (distanceBlack < 80 || distanceWhite < 80) {
			cameraShaker.shakeAmount = 0.4f;
		}
		else if (distanceBlack < 70 || distanceWhite < 70) {
			cameraShaker.shakeAmount = 0.6f;
		} else if (distanceBlack < 50 || distanceWhite < 50) {
			cameraShaker.shakeAmount = 1.0f;
		} else if (distanceBlack < 30 || distanceWhite < 30) {
			cameraShaker.shakeAmount = 2.0f;
		} else {
			cameraShaker.shakeAmount = 0.0f;
		}

//		if (textObject != null) {
//			if (textObject.GetComponent<RectTransform>().position.y >= 1400) {
//				LevelManager.LoadRoom ("startMenu");
//			}
//		}
	}


	void OnTriggerEnter(Collider col){


		gameObject.GetComponent<CustomFirstPersonController> ().shouldWalk = false;
		gameObject.GetComponent<CustomFirstPersonController> ().shouldLookAround = false;

		if (mainCamera.transform.rotation.eulerAngles.y >= 0 &&
		    mainCamera.transform.rotation.eulerAngles.y < 180) {
			Debug.Log ("BLACK");
			creditCanvas.GetComponent<Image> ().color = Color.black;

			creditCanvas.enabled = true;
			textObject = creditCanvas.transform.FindChild("Text");
			creditText = textObject.GetComponent<Text> ();
			creditText.color = Color.white;
			creditText.GetComponent<CreditsTranslation> ().StartCredits ();
		} else {
			Debug.Log ("WHITE");
			creditCanvas.GetComponent<Image> ().color = Color.white;

			creditCanvas.enabled = true;
			textObject = creditCanvas.transform.FindChild("Text");
			creditText = textObject.GetComponent<Text> ();
			creditText.color = Color.black;
			creditText.GetComponent<CreditsTranslation> ().StartCredits ();

		}

	}
}
