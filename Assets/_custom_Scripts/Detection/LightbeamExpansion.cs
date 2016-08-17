using UnityEngine;
using System.Collections;

public class LightbeamExpansion : MonoBehaviour
{
	public Transform Expand() {
		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.up);
		if (Physics.Raycast (ray, out hit, 100)) {
			float dist = hit.distance;
			Debug.DrawRay (ray.origin, ray.direction*dist, Color.green, 10.0f);

			// Object specific distance adjustments
			if (hit.transform.name == "crystal_large") dist += 0.2f;
			else if (hit.transform.name == "plate_grp_v") dist += 0.12f;
			else if (hit.transform.name == "gold_plate_h") dist += 0.13f;
			else if (hit.transform.name == "crystal_small") dist += 0.08f;

			transform.localScale = new Vector3 (1, dist, 1);
			return hit.transform;
		}
		transform.localScale = new Vector3 (1, 100, 1);
		return null;
	}
}
