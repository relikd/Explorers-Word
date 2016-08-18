using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO; 



 public class UserInput : MonoBehaviour {

	public string TriggerTag;
	Queue visibleObjects = new Queue();
	private int QueueLength = 3;

	LinkedList<GameObject> visibleObjects2 = new LinkedList<GameObject>();

	void Start() {
		
	}



	public void handleUserInput(string UserInput) {
		if (UserInput == "all") {
			deactivateAllGameObjects ();
		}

//		GameObject gameObject = GameObject.Find (UserInput);
//		if (gameObject) {
//			deactivateObject (gameObject);
//		}
	
		if (!handleGameObjectInQueue (UserInput)) {
			disableGameObjectViaTag (UserInput);
		}
		ResetInputField();
	}

	private void updateQueueWithGameObject(GameObject go) {
		if (visibleObjects2.Count == QueueLength) {
			
			deactivateObject (visibleObjects2.First.Value, false);
			visibleObjects2.RemoveFirst();
			visibleObjects2.AddLast (go);
		} else {
			visibleObjects2.AddLast (go);
		}
	}

	private void deactiveQueueObjects() {
		foreach (GameObject go in visibleObjects2) {
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

	private bool handleGameObjectInQueue(string text)
	{
		bool result = false;
		foreach (GameObject go in visibleObjects2) {
			if (go.name == text) {
				deactivateObject (go, false);
				visibleObjects2.Remove (go);
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