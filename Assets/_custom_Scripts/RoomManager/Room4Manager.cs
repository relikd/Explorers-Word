using UnityEngine;
using System.Collections;


namespace RoomManager{
	/// <summary>
	/// Manages the level exit in room 4 and statue rigidbody active state
	/// </summary>
	public class Room4Manager : MonoBehaviour {

		public GameObject statue;
		public GameObject platform;
		public GameObject player;


		/// <summary>
		/// Checks if player is below y = 2 and leaves room if true. Sets active state of statues rigidbody
		/// </summary>
		void Update () {
			if (player.transform.position.y < 2) {
				LevelManager.LeaveRoom ();
			}

			if (statue.GetComponent<Renderer> ().enabled &&
			    platform.GetComponent<Renderer> ().enabled) {
				statue.GetComponent<Rigidbody> ().isKinematic = false;
				statue.GetComponent<Rigidbody> ().useGravity = true;
			} else {
				statue.GetComponent<Rigidbody> ().isKinematic = true;
				statue.GetComponent<Rigidbody> ().useGravity = false;
			}
		}
	}

}