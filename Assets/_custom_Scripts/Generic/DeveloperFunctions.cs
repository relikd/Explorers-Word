using UnityEngine;
using UnityEngine.SceneManagement;

/** Tools which help debugging the game for further analysis */
namespace XplrDebug
{
	/**
	 * A bunch of functions to make level debugging easier
	 */
	public class DeveloperFunctions : MonoBehaviour
	{
		/**
		 * Disable all wall collider etc.
		 */
		public static void toggleCollissions() {
			Object[] gameObjects = Object.FindObjectsOfType (typeof(GameObject));
			foreach (Object obj in gameObjects) {
				GameObject objG = (GameObject)obj;
				string n = objG.name.ToLower ();
				if (n!="floor" && n!="ground" && n!="base" && 
					n!="firstpersoncharacter" && n!="player" && n!="fpscontroller") {
					Collider[] colliders = objG.GetComponents<Collider> ();
					foreach (Collider c in colliders) {
						c.enabled = !c.enabled;
					}
				}
			}
		}
		/**
		 * Just reload the same room
		 */
		public static void resetRoom() {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}

