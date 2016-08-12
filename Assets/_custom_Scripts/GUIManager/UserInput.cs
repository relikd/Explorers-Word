using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO; 



 public class UserInput : MonoBehaviour {

	public string TriggerTag;

	public void handleUserInput(string UserInput) {
		if (UserInput == "all") {
			deactivateAllGameObjects ();
		}

		GameObject gameObject = GameObject.Find (UserInput);
		if (gameObject) {
			deactivateObject (gameObject);
		}
	}

	private void deactivateObject(GameObject go) {
		Collider[] colliders = go.GetComponentsInChildren<Collider> ();
		foreach (Collider c in colliders) {
			c.enabled = !c.enabled;
		}
		Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = !r.enabled;
		}
		Rigidbody rigidbody = go.GetComponent<Rigidbody> ();
		if (rigidbody) {
			rigidbody.useGravity = !rigidbody.useGravity;
		}

	}

	public void deactivateAllGameObjects() {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(TriggerTag);
		Debug.Log (gameObjects.Length);

		foreach (GameObject go in gameObjects) {
			if (go.activeInHierarchy) {
				deactivateObject (go);
			}
		}
	}
}