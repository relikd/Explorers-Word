using UnityEngine;
using UnityTest;

namespace XplrDebug
{
	/**
	 * Checks if the assigned GameObject was removed from scene (used for integration tests).
	 */
	public class TESTObjectExist : MonoBehaviour
	{
		[SerializeField] private float checkTime = 0.3f;
		[SerializeField] public GameObject goReference;

		private float time = 0.0f;
		private bool shouldCheck = true;
		/**
		 * Check the object after the specified time period
		 */
		public void Update() {
			time += Time.deltaTime;
			if (shouldCheck && time > checkTime) {
				shouldCheck = false;
				checkExistance ();
			}
		}
		/**
		 * Check if {@link #goReference} is null and throw Assertion
		 */
		void checkExistance() {
			if (goReference == null) {
				var gc = AssertionComponent.Create<GeneralComparer> (CheckMethod.Update, gameObject, "TESTObjectExist.goReference", null);
				gc.compareType = GeneralComparer.CompareType.AEqualsB;
			}
		}
	}
}