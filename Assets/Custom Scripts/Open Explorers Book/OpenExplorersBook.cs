using UnityEngine;
using System.Collections;

namespace ExplorersBook
{
	public class OpenExplorersBook : MonoBehaviour
	{

		void LateUpdate() {
		
			if (Input.GetKeyUp(KeyCode.B)) {
				gameObject.SetActive (!gameObject.activeSelf);
			}
		}
	}
}

