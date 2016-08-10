using UnityEngine;
using System.Collections;

namespace Debugging
{
	public class DebuggingInterface : MonoBehaviour
	{
		private bool isActive = false;
		public GameObject InputField;
		void onStart(){
		}

		public void HandleUserInput(string code) {
			switch (code) {
			case "collisions":
				toggelCollissions ();
				break;
			case "visibility":
				toggelVisibility ();
				break;
			case "light":
				toggelLigth ();
				break;
			case "YourCase":

				break;
			default: break;
			}
		}

		void LateUpdate() {
			if (Input.GetKeyUp (KeyCode.Caret)) {
				isActive = !isActive;
				DisablePlayerMovement ();
				UnlockMouseMovement ();
				InputField.SetActive (isActive);
			}
		}

		private void toggelLigth() {
			Object[] gameObjects = Object.FindObjectsOfType (typeof(GameObject));
			foreach (Object obj in gameObjects) {
				GameObject objG = (GameObject)obj;
				Light[] colliders = objG.GetComponents<Light> ();
				foreach (Light c in colliders) {
					c.enabled = !c.enabled;
				}
			}
		}

		private void toggelCollissions() {
			Object[] gameObjects = Object.FindObjectsOfType (typeof(GameObject));
			foreach (Object obj in gameObjects) {
				GameObject objG = (GameObject)obj;
				Collider[] colliders = objG.GetComponents<Collider> ();
				foreach (Collider c in colliders) {
					c.enabled = !c.enabled;
				}
			}
		}

		private void toggelVisibility() {
			Object[] gameObjects = Object.FindObjectsOfType (typeof(GameObject));
			foreach (Object obj in gameObjects) {
				GameObject objG = (GameObject)obj;
				Renderer[] colliders = objG.GetComponents<Renderer> ();
				foreach (Renderer c in colliders) {
					c.enabled = !c.enabled;
				}
			}
		}

		private void toggelBottomCollider(){
			GameObject bottom = GameObject.FindGameObjectWithTag ("Bottom");
			Collider bottomCollider = bottom.GetComponent<Collider> ();
			bottomCollider.enabled = !bottomCollider.enabled;
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


	}
}

