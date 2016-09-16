using UnityEngine;
using UnityEngine.UI;

namespace RoomManager
{
	public class RoomEndOfGameManager : MonoBehaviour {

		public Canvas creditCanvas;
		public Camera mainCamera;
		public Transform whitePortalBorder;
		public Transform blackPortalBorder;
		private CameraShakeScript cameraShaker;
		private Transform textObject;
		private Text creditText;
		public RawImage whiteIcon;
		public RawImage blackIcon;

		private float creditsHeight = 0.0f;
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


			if (textObject != null) {
				Debug.Log (Screen.currentResolution.height.ToString ());
				if (textObject.GetComponent<RectTransform>().position.y >= 
					Screen.currentResolution.height + creditsHeight/2 && 
					textObject.GetComponent<RectTransform>().position.y != 0.0f) {
					LevelManager.LoadStartScreen ();
				}
			}
		}

		void OnTriggerEnter(Collider col){

			XplrCharacter.FPSController fps_script = GetComponent<XplrCharacter.FPSController> ();
			fps_script.shouldWalk = false;
			fps_script.shouldJump = false;
			fps_script.shouldLookAround = false;

			float lookDirection = mainCamera.transform.rotation.eulerAngles.y;
			bool blackEnd = (lookDirection >= 0 && lookDirection < 180);
			XplrDebug.LogWriter.Write ("Game End: "+ (blackEnd ? "BLACK" : "WHITE"));

			// background color
			creditCanvas.GetComponent<Image> ().color = (blackEnd ? Color.black : Color.white);
			creditCanvas.enabled = true;
			textObject = creditCanvas.transform.FindChild("Text");
			creditText = textObject.GetComponent<Text> ();
			// text color
			creditText.color = (blackEnd ? Color.white : Color.black);
			creditText.GetComponent<CreditsTranslation> ().StartCredits ();

			creditsHeight = creditText.GetComponent<RectTransform> ().rect.height;

			if (creditText.color == Color.white) {
				Destroy (blackIcon);
				} else {
				Destroy (whiteIcon);
			}

		}
	}
}