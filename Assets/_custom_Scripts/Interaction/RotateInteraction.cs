using UnityEngine;
using System.Collections;
using Interaction;

public class RotateInteraction : Interactable
{
	public Vector3 rotateBy = new Vector3(0,10,0);
	[SerializeField]
	private bool stepAngle = false;

	override public string interactMessage() {
		return "rotate";
	}

	override public void OnInteractionKeyPressed() {
		if (stepAngle) rotate ();
	}

	override public void OnInteractionKeyHold() {
		if (!stepAngle) rotate ();
	}

	void rotate() {
		gameObject.transform.Rotate (rotateBy);
		checkForRotationLimit ();
	}

	// Rotation Limiter Script Extension
	void checkForRotationLimit() {
		RotationLimiter rl_script = gameObject.GetComponent<RotationLimiter>();
		if (rl_script) {
			int ool = rl_script.outOfLimit ();
			int dol = rl_script.directlyOnLimit ();
			int edge = ool | dol;
			if ((edge & 1)!=0) rotateBy.x *= -1;
			if ((edge & 2)!=0) rotateBy.y *= -1;
			if ((edge & 4)!=0) rotateBy.z *= -1;

			if (ool > 0) {
				gameObject.transform.Rotate (rotateBy);
				if (rl_script.directlyOnLimit () > 0)
					gameObject.transform.Rotate(rotateBy);
			}
		}
	}
}