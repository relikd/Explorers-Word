using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour {

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	public float shakeAmount = 0.7f;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

	}
}
