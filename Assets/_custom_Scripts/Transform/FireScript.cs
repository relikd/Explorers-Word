using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	private bool isGettingBrighter;
	private float currentValue;
	private Light lightsource;
	[SerializeField]public float minimum;
	[SerializeField]public bool randomValue;
	[SerializeField]public float stepSize;
	[SerializeField]public float maximum;

	// Use this for initialization
	void Start () {
		lightsource = gameObject.GetComponent<Light> ();
		isGettingBrighter = true;
		currentValue = minimum;
	}
	
	// Update is called once per frame
	void Update () {
		if (randomValue) {
			lightsource.intensity = Random.Range (minimum, maximum);
		} else {
			ChangeLightIntensity ();
		}
	}

	private void ChangeLightIntensity(){
		if (isGettingBrighter) 
		{
			if (currentValue > maximum) {
				isGettingBrighter = false;
			}else
			{
				lightsource.intensity = currentValue;
				currentValue = currentValue + stepSize;
			}
		}
		if (!isGettingBrighter) {
			if (currentValue < minimum) {
				isGettingBrighter = true;
			}else
			{
				lightsource.intensity = currentValue;
				currentValue = currentValue - stepSize;
			}
		}
	}
}

