using UnityEngine;
using System.Collections;

namespace ContinuousTransformation
{
	/** Create a fire like light flicker */
	public class FireScript : MonoBehaviour {

		private bool isGettingBrighter;
		private float currentValue;
		private Light lightsource;
		[SerializeField] public float minimum;
		[SerializeField] public bool randomValue;
		[SerializeField] public float stepSize;
		[SerializeField] public float maximum;

		/** Save light source and preset variables */
		void Start () {
			lightsource = gameObject.GetComponent<Light> ();
			isGettingBrighter = true;
			currentValue = minimum;
		}

		/** Randomize light flicker every frame */
		void Update () {
			if (randomValue) {
				lightsource.intensity = Random.Range (minimum, maximum);
			} else {
				ChangeLightIntensity ();
			}
		}

		/** Bounce brightness between min and max */
		private void ChangeLightIntensity(){
			if (isGettingBrighter) {
				if (currentValue > maximum) {
					isGettingBrighter = false;
				} else {
					lightsource.intensity = currentValue;
					currentValue = currentValue + stepSize;
				}
			}
			if (!isGettingBrighter) {
				if (currentValue < minimum) {
					isGettingBrighter = true;
				} else {
					lightsource.intensity = currentValue;
					currentValue = currentValue - stepSize;
				}
			}
		}
	}
}