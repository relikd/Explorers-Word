using UnityEngine;
using System.Collections;

namespace ContinuousTransformation
{
	/** Slowly moves water up and down */
	public class WaterMovementScript : MonoBehaviour {

		public float movementSize;
		public Vector3 movementAxis;
		private int moveDir = -1;
		public int steps = 20;

		private int counter = 0;

		/** Move water up and down */
		void FixedUpdate () {
			if (moveDir == 1) {
				this.transform.position += new Vector3 (
					movementAxis.x * movementSize,
					movementAxis.y * movementSize,
					movementAxis.z * movementSize
				);
				counter++;
			}
			if (moveDir == -1) {
				this.transform.position -= new Vector3 (
					movementAxis.x * movementSize,
					movementAxis.y * movementSize,
					movementAxis.z * movementSize
				);
				counter++;
			}

			if (counter >= steps) {
				counter = 0;
				moveDir = moveDir * (-1);
			}
		}
	}
}