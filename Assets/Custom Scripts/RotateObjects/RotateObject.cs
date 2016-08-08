using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AssemblyCSharp
{
	public class RotateObject : MonoBehaviour
	{
		public int degree;
		public bool rotateXAxis = false;
		public bool rotateYAxis = true;
		public bool rotateZAxis = false;

		void LateUpdate() {
			if (Input.GetKey (KeyCode.E)) {
				gameObject.transform.Rotate(rotateXAxis?degree:0,rotateYAxis?degree:0,rotateZAxis?degree:0);

			}
		}

		void OnGUI ()
		{
			// Access InReach variable from raycasting script.
			GameObject Player = GameObject.Find("FirstPersonCharacter");
			Reachable detection = Player.GetComponent<Reachable>();

			if (detection.InReach == true && detection.RaycastHit.collider.tag == gameObject.tag)
			{
				GUI.color = Color.white;
				GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'E' to rotate");
			}
		}
	}
}

