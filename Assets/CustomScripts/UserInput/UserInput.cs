using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO; 
using AssemblyCSharp;



 public class UserInput : MonoBehaviour {
	public void handleUserInput(string UserInput) {
		Debug.Log (UserInput);
		GameObject gameObject = GameObject.Find (UserInput);
		if (gameObject) {

			Collider[] colliders = gameObject.GetComponentsInChildren<Collider> ();
			foreach (Collider c in colliders) {
				c.enabled = !c.enabled;
			}
			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers) {
				r.enabled = !r.enabled;
			}
		}
	}
}