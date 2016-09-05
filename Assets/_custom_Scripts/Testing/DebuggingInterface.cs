using UnityEngine;
using System.Collections;

namespace Debugging
{
	public class DebuggingInterface : MonoBehaviour
	{
		private bool isActive = false;
		public GameObject InputField;

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
	}
}

