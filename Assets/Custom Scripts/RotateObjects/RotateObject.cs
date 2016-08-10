﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Interaction
{
	public class RotateObject : MonoBehaviour, Rotatable
	{
		private RotationLimiter rl_script;

		public float rotateXAxisBy = 0;
		public float rotateYAxisBy = 10;
		public float rotateZAxisBy = 0;
		public bool stepAngle = false;
		[HideInInspector] bool shouldDepictText;

		void LateUpdate() {
			
		}

		public void HandleRaycastCollision(){
//			if (stepAngle ? Input.GetKeyUp(KeyCode.E) : Input.GetKey(KeyCode.E)) {
			Debug.Log("asdfasdfasdfasdfasdf");
				gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);

				if (rl_script) {
					int ool = rl_script.outOfLimit ();
					if ((ool & 1)!=0) rotateXAxisBy *= -1;
					if ((ool & 2)!=0) rotateYAxisBy *= -1;
					if ((ool & 4)!=0) rotateZAxisBy *= -1;

					if (ool > 0 && !rl_script.isDirectlyOnLimit())
						gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);
				}
				shouldDepictText = false;
//			}
		}

		public void EnableGUI(bool enable) {
			shouldDepictText = enable;

		}
			
		void Start () {
			rl_script = gameObject.GetComponent<RotationLimiter>();
		}

		void OnGUI ()
		{
			if (shouldDepictText)
			{
				GUI.color = Color.white;
				GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2) + 10, 200, 25), "Press 'R' to rotate");
			}
		}
	}
}

