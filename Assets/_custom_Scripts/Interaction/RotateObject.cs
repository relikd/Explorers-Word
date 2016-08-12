using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Interaction
{
	public class RotateObject : MonoBehaviour, Interactable
	{
		private RotationLimiter rl_script;

		public float rotateXAxisBy = 0;
		public float rotateYAxisBy = 10;
		public float rotateZAxisBy = 0;
		public bool stepAngle = false;

		void LateUpdate() {
			
		}

		public void HandleRaycastCollission(){
		if (stepAngle ? Input.GetKeyUp(KeyCode.E) : Input.GetKey(KeyCode.E)) {
				gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);

				if (rl_script) {
					int ool = rl_script.outOfLimit ();
					int dol = rl_script.directlyOnLimit ();
					int edge = ool | dol;
					if ((edge & 1)!=0) rotateXAxisBy *= -1;
					if ((edge & 2)!=0) rotateYAxisBy *= -1;
					if ((edge & 4)!=0) rotateZAxisBy *= -1;

					if (ool > 0) {
						gameObject.transform.Rotate (rotateXAxisBy, rotateYAxisBy, rotateZAxisBy);
						if (rl_script.directlyOnLimit () > 0)
							gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);
					}
				}
		}
		}

		public void EnableGUI(bool enable) {
			GameObject player = GameObject.Find ("FirstPersonCharacter");
			if (player) {
				player.GetComponent<GUIManager> ().register ("Press 'E' to rotate", enable);
			}
		}
			
		void Start () {
			rl_script = gameObject.GetComponent<RotationLimiter>();
		}

		void OnGUI ()
		{

		}
	}
}

