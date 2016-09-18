using UnityEngine;

/**
 * Limit the rotation of an {@link RotateInteraction} script
 * @see RotateInteraction
 */
[RequireComponent(typeof(Interaction.RotateInteraction))]
public class RotationLimiter : MonoBehaviour {

	/** Limits are [-180 to 180] if relative and [0 to 360] if absolute */
	[SerializeField] private Vector3 minAngle = new Vector3(-180,-60,-180);
	/** Limits are [-180 to 180] if relative and [0 to 360] if absolute */
	[SerializeField] private Vector3 maxAngle = new Vector3(180,30,180);
	/** either limit relative to initial rotation or absolute 0-360 */
	[SerializeField] private bool absoluteAngle = false;

	private Quaternion initialRotation;

	/**
	 * Save the initial rotation for a relative rotation limiter
	 */
	void Awake () {
		initialRotation = gameObject.transform.rotation;
	}

	/**
	 * Returns a bitmask with one bit for each angle, if out of limit
	 * @return Bitmask with 0 = in bounds, >0 = out of limit (x|=1, y|=2, z|=4)
	 */
	public int outOfLimit () {
		int outCode = 0;
		Vector3 deg = getCurrentRotation ();
		if (absoluteAngle) {
			if (deg.x > maxAngle.x+0.0001F || deg.x < minAngle.x-0.0001F) { outCode |= 1; }
			if (deg.y > maxAngle.y+0.0001F || deg.y < minAngle.y-0.0001F) { outCode |= 2; }
			if (deg.z > maxAngle.z+0.0001F || deg.z < minAngle.z-0.0001F) { outCode |= 4; }
		} else {
			if (deg.x > maxAngle.x+0.0001F && deg.x < minAngle.x+359.9999F) { outCode |= 1; }
			if (deg.y > maxAngle.y+0.0001F && deg.y < minAngle.y+359.9999F) { outCode |= 2; }
			if (deg.z > maxAngle.z+0.0001F && deg.z < minAngle.z+359.9999F) { outCode |= 4; }
		}
		return outCode;
	}

	/**
	 * Same like {@link #outOfLimit()} but bit will be set if directly on border
	 * @return Bitmask same like {@link #outOfLimit()}
	 * @see #outOfLimit()
	 */
	public int directlyOnLimit() {
		int outCode = 0;
		Vector3 deg = getCurrentRotation ();
		if (Mathf.Min( Mathf.Abs(deg.x-maxAngle.x), Mathf.Abs(deg.x-minAngle.x-(absoluteAngle?0:360)) ) < 0.0001F) { outCode |= 1; }
		if (Mathf.Min( Mathf.Abs(deg.y-maxAngle.y), Mathf.Abs(deg.y-minAngle.y-(absoluteAngle?0:360)) ) < 0.0001F) { outCode |= 2; }
		if (Mathf.Min( Mathf.Abs(deg.z-maxAngle.z), Mathf.Abs(deg.z-minAngle.z-(absoluteAngle?0:360)) ) < 0.0001F) { outCode |= 4; }
		return outCode;
	}
	/**
	 * Get the rotation difference between current and initial state
	 * @return rotation difference
	 */
	private Vector3 getCurrentRotation() {
		if (absoluteAngle) return gameObject.transform.rotation.eulerAngles;
		return (Quaternion.Inverse(initialRotation) * gameObject.transform.rotation).eulerAngles;
	}
}