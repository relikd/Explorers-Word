using UnityEngine;
using System.Collections;

namespace ContinuousTransformation
{
	/** Used in main menu for displaying moving mountains in the background */
	public class ContinousCameraRotationMainMenu : MonoBehaviour {
		/** Rotation happens every frame */
		void Update () {
			transform.Rotate (new Vector3 (0, 0.1f, 0));
		}
	}
}