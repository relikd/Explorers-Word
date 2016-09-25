using UnityEngine;
using System.Collections;

namespace ContinuousTransformation
{
	/** Perform a continous rotation with Inspector defined rotation axis */
	public class ContinousRotationScript : MonoBehaviour {
		public Vector3 rotationAxis;
		/** Continuously rotate around provided axis */
		void Update () {
			this.gameObject.GetComponent<Transform> ().Rotate (rotationAxis);
		}
	}
}