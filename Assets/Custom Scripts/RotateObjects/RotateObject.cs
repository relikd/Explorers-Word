using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AssemblyCSharp
{
	public class RotateObject : MonoBehaviour
	{
		private RotationLimiter rl_script;

		public float rotateXAxisBy = 0;
		public float rotateYAxisBy = 10;
		public float rotateZAxisBy = 0;
		public bool stepAngle = false;

		void LateUpdate() {
			if (stepAngle ? Input.GetKeyUp(KeyCode.E) : Input.GetKey(KeyCode.E)) {

				gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);

				if (rl_script) {
					int ool = rl_script.outOfLimit ();
					if ((ool & 1)!=0) rotateXAxisBy *= -1;
					if ((ool & 2)!=0) rotateYAxisBy *= -1;
					if ((ool & 4)!=0) rotateZAxisBy *= -1;

					if (ool > 0 && !rl_script.isDirectlyOnLimit())
						gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);
				}
			}
		}

		void Start () {
			rl_script = gameObject.GetComponent<RotationLimiter>();
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

