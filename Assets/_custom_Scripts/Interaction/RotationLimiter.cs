using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RotateInteraction))]
public class RotationLimiter : MonoBehaviour {

	public float minAngle = -60;
	public float maxAngle = 60;
	private Quaternion initialRotation;

	public bool limitX = false;
	public bool limitY = true;
	public bool limitZ = false;

	// Use this for initialization
	void Awake () {
		initialRotation = gameObject.transform.rotation;
	}

	// return 0 = fine, 1 = x out of limit, 2 = y, 4 = z
	public int outOfLimit () {
		Quaternion relative = Quaternion.Inverse(initialRotation) * gameObject.transform.rotation;
		float degX = relative.eulerAngles.x;
		float degY = relative.eulerAngles.y;
		float degZ = relative.eulerAngles.z;

		int outCode = 0;
		if (limitX && degX > maxAngle+0.0001F && degX < minAngle+359.9999F) { outCode |= 1; }
		if (limitY && degY > maxAngle+0.0001F && degY < minAngle+359.9999F) { outCode |= 2; }
		if (limitZ && degZ > maxAngle+0.0001F && degZ < minAngle+359.9999F) { outCode |= 4; }

		return outCode;
	}

	public int directlyOnLimit() {
		Quaternion relative = Quaternion.Inverse(initialRotation) * gameObject.transform.rotation;
		float degX = relative.eulerAngles.x;
		float degY = relative.eulerAngles.y;
		float degZ = relative.eulerAngles.z;

		int outCode = 0;
		if (limitX && Mathf.Min( Mathf.Abs(degX-maxAngle), Mathf.Abs(degX-360-minAngle) ) < 0.0001F) { outCode |= 1; }
		if (limitY && Mathf.Min( Mathf.Abs(degY-maxAngle), Mathf.Abs(degY-360-minAngle) ) < 0.0001F) { outCode |= 2; }
		if (limitZ && Mathf.Min( Mathf.Abs(degZ-maxAngle), Mathf.Abs(degZ-360-minAngle) ) < 0.0001F) { outCode |= 4; }

		return outCode;
	}
}
