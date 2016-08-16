using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RotateInteraction))]
public class RotationLimiter : MonoBehaviour {

	[SerializeField] private Vector3 minAngle = new Vector3(-180,-60,-180);
	[SerializeField] private Vector3 maxAngle = new Vector3(180,30,180);
	[SerializeField] private bool absoluteAngle = false;

	private Quaternion initialRotation;

	// Use this for initialization
	void Awake () {
		initialRotation = gameObject.transform.rotation;
	}

	// return 0 = fine, 1 = x out of limit, 2 = y, 4 = z
	public int outOfLimit () {
		int outCode = 0;
		Vector3 deg = getCurrentRotation ();
		if (deg.x > maxAngle.x+0.0001F && deg.x < minAngle.x+359.9999F) { outCode |= 1; }
		if (deg.y > maxAngle.y+0.0001F && deg.y < minAngle.y+359.9999F) { outCode |= 2; }
		if (deg.z > maxAngle.z+0.0001F && deg.z < minAngle.z+359.9999F) { outCode |= 4; }
		return outCode;
	}

	public int directlyOnLimit() {
		int outCode = 0;
		Vector3 deg = getCurrentRotation ();
		if (Mathf.Min( Mathf.Abs(deg.x-maxAngle.x), Mathf.Abs(deg.x-360-minAngle.x) ) < 0.0001F) { outCode |= 1; }
		if (Mathf.Min( Mathf.Abs(deg.y-maxAngle.y), Mathf.Abs(deg.y-360-minAngle.y) ) < 0.0001F) { outCode |= 2; }
		if (Mathf.Min( Mathf.Abs(deg.z-maxAngle.z), Mathf.Abs(deg.z-360-minAngle.z) ) < 0.0001F) { outCode |= 4; }
		return outCode;
	}

	private Vector3 getCurrentRotation() {
		if (absoluteAngle) return gameObject.transform.rotation.eulerAngles;
		return (Quaternion.Inverse(initialRotation) * gameObject.transform.rotation).eulerAngles;
	}
}
