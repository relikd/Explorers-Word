using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO; 
using System;

[Serializable]
public class NamedObject {
	public GameObject thing;
	public string[] names;
}

 public class UserInput : MonoBehaviour {

	public string TriggerTag;
	private int QueueLength = 3;
	LinkedList<GameObject> visibleObjects = new LinkedList<GameObject>();

	public NamedObject[] objects;

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
		}
			visibleObjects.AddLast (go);
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
		go.transform.hasChanged = true;
	}

	public void deactivateAllGameObjects() {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(TriggerTag);
		foreach (GameObject go in gameObjects) {
			if (go.activeInHierarchy) {
				deactivateObject (go, true);
				go.transform.hasChanged = true;
			}
		}
	}

	private bool handleGameObjectInQueue(string text) {
		bool result = false;
		GameObject goToRemove = new GameObject();
		foreach (GameObject go in visibleObjects) {
			if (go.name == text) {
				deactivateObject (go, false);
				goToRemove = go;
				result = true;
			}
		}
		if (goToRemove) {
			visibleObjects.Remove (goToRemove);
		}
		return result;
	}

	private void disableGameObjectViaTag(string text) {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (TriggerTag);
		foreach (GameObject go in gameObjects) {
			if (go.activeInHierarchy && go.name == text) {
				updateQueueWithGameObject (go);
				deactivateObject (go, true);
			}
		}
	}
		
}