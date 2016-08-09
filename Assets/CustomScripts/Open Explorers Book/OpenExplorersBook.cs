using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{
		public GameObject explBook;
		public RectTransform canvas;
		public RectTransform UI_Element;
		public Camera cammera;

		private bool isActive = false;
		private bool rotated = false;

		void LateUpdate() {

			if (Input.GetKeyUp(KeyCode.B)) {
				
				isActive = !isActive;
				explBook.SetActive (isActive);
				DisablePlayerMovement ();
				UnlockMouseMovement ();
			}
		}

		private void DisablePlayerMovement() {
			GameObject player = GameObject.Find ("FPSController");
			player.GetComponent<CharacterController>().enabled = !isActive;
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = !isActive;
		}

		private void UnlockMouseMovement() {
			GameObject FirstPersonCharacter = GameObject.Find ("FirstPersonCharacter");
			MouseLock mouseLock = FirstPersonCharacter.GetComponent<MouseLock>();
			mouseLock.enabled = !isActive;
			Cursor.visible = isActive;
			Cursor.lockState = CursorLockMode.Confined;
			FirstPersonCharacter.GetComponent<MouseCrosshair> ().enabled = !isActive;
		}

		void OnGUI() {
			//GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "Here is a block of text\nlalalala\nanother line\nI could do this all day!");
			if (!rotated) { 
				Vector3 explorersBookRotation = explBook.transform.rotation.eulerAngles;
//				Vector3 explorersBookTranslation = explBook.transform.position;
//				TextPanel.transform.position = explorersBookTranslation;
//				TextPanel.transform.Rotate (explorersBookRotation);
				UI_Element.transform.Rotate(explorersBookRotation);
				RectTransform CanvasRect = canvas.GetComponent<RectTransform> ();

				Vector2 ViewportPosition = cammera.WorldToViewportPoint (explBook.transform.position);
				Vector2 WorldObject_ScreenPosition = new Vector2 (
					((ViewportPosition.x * canvas.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
					((ViewportPosition.x * canvas.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));


				Vector2 BookScreenCoord = RectTransformUtility.WorldToScreenPoint (cammera, explBook.transform.position);


				UI_Element.anchoredPosition3D = WorldObject_ScreenPosition;
				 

				//UI_Element.transform.SetParent(explBook.transform);
				rotated = true;
			}
		}

	}
}

