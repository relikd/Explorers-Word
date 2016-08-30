using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour {
	public CursorLockMode wantedMode;

	// Apply requested cursor state
	void SetCursorState ()
	{
		//wantedMode = CursorLockMode.Locked;
		Cursor.lockState = wantedMode;
		// Hide cursor when locking
		if (Cursor.lockState == CursorLockMode.Confined) 
			Cursor.visible = (CursorLockMode.Locked != wantedMode);
	} 

	void OnGUI ()
	{
//		// Release cursor on escape keypress
//		if (Input.GetKeyDown (KeyCode.Escape))
//			Cursor.lockState = wantedMode = CursorLockMode.Confined;
//
		if (Input.GetKeyDown (KeyCode.Mouse0))
			Cursor.lockState = wantedMode = CursorLockMode.Locked;
		
		SetCursorState ();
	}
}