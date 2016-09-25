using UnityEngine;
using System.Collections;

/**
 * Scripts that repeatedly perform the same object transformation
 */
namespace ContinuousTransformation
{
	/**
	 * Vividly move camera back and forth to simulate excitement (used in end scene)
	 */
	public class CameraShakeScript : MonoBehaviour {
		/** Transform of the camera to shake. Grabs the gameObject's transform if null */
		public Transform camTransform;

		public float shakeAmount = 0.7f;
		Vector3 originalPos;

		/** Assign gameObject's transform if none provided */
		void Awake() {
			if (camTransform == null)
				camTransform = GetComponent(typeof(Transform)) as Transform;
		}
		/** Save original position */
		void OnEnable() {
			originalPos = camTransform.localPosition;
		}
		/** Shake camera */
		void Update() {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		}
	}
}