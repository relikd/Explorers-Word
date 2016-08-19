using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO; 



 public class UserInput : MonoBehaviour {

	public string TriggerTag;
	private int QueueLength = 3;
	LinkedList<GameObject> visibleObjects = new LinkedList<GameObject>();

	public void handleUserInput(string UserInput) {
		if (UserInput == "all") {
			deactivateAllGameObjects ();
		}
	
		if (!handleGameObjectInQueue (UserInput)) {
			disableGameObjectViaTag (UserInput);
		}

		ResetInputField();
	}

	private void updateQueueWithGameObject(GameObject go) {
		if (visibleObjects.Count == QueueLength) {
			deactivateObject (visibleObjects.First.Value, false);
			visibleObjects.RemoveFirst();
			visibleObjects.AddLast (go);
		} else {
			visibleObjects.AddLast (go);
		}
	}

	private void deactiveQueueObjects() {
		foreach (GameObject go in visibleObjects) {
			deactivateObject (go, true);
		}
	}

	private void ResetInputField() {
		InputField inputFieldComponent = GameObject.Find("ExplorersWord").GetComponent<InputField> ();
		if (inputFieldComponent) {
			inputFieldComponent.ActivateInputField ();
			inputFieldComponent.text = "" ;
		}
	}

	private void deactivateObject(GameObject go, bool deactivate) {
		Collider[] colliders = go.GetComponentsInChildren<Collider> ();
		foreach (Collider c in colliders) {
			c.enabled = !deactivate;
		}
		Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = !deactivate;
		}
		Rigidbody rigidbody = go.GetComponent<Rigidbody> ();
		if (rigidbody) {
			rigidbody.useGravity = !deactivate;
		}
	}

	public void deactivateAllGameObjects() {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(TriggerTag);
		foreach (GameObject go in gameObjects) {
			if (go.activeInHierarchy) {
				deactivateObject (go, true);
				go.transform.hasChanged = !go.transform.hasChanged;
			}
		}
	}

	private bool handleGameObjectInQueue(string text) {
		bool result = false;
		foreach (GameObject go in visibleObjects) {
			if (go.name == text) {
				deactivateObject (go, false);
				visibleObjects.Remove (go);
				result = true;
			}
		}
		return result;
	}

	private void disableGameObjectViaTag(string text) {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (TriggerTag);
		foreach (GameObject go in gameObjects) {
			if (go.activeInHierarchy && go.name == text) {
				updateQueueWithGameObject (go);
				deactiveQueueObjects();
				go.transform.hasChanged = !go.transform.hasChanged;
			}
		}
	}
		
}