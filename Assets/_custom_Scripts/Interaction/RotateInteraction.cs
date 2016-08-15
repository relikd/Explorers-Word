using UnityEngine;
using System.Collections;
using Interaction;

public class RotateInteraction : Interactable
{
	public float rotateXAxisBy = 0;
	public float rotateYAxisBy = 10;
	public float rotateZAxisBy = 0;
	public bool stepAngle = false;

	override public string interactMessage() {
		return "rotate";
	}

	override public void interactionKeyPressed() {
		if (stepAngle) rotate ();
	}

	override public void interactionKeyHold() {
		if (!stepAngle) rotate ();
	}

	void rotate() {
		gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);
		checkForRotationLimit ();
	}

	// Rotation Limiter Script Extension
	void checkForRotationLimit() {
		RotationLimiter rl_script = gameObject.GetComponent<RotationLimiter>();
		if (rl_script) {
			int ool = rl_script.outOfLimit ();
			int dol = rl_script.directlyOnLimit ();
			int edge = ool | dol;
			if ((edge & 1)!=0) rotateXAxisBy *= -1;
			if ((edge & 2)!=0) rotateYAxisBy *= -1;
			if ((edge & 4)!=0) rotateZAxisBy *= -1;

			if (ool > 0) {
				gameObject.transform.Rotate (rotateXAxisBy, rotateYAxisBy, rotateZAxisBy);
				if (rl_script.directlyOnLimit () > 0)
					gameObject.transform.Rotate(rotateXAxisBy,rotateYAxisBy,rotateZAxisBy);
			}
		}
	}
}