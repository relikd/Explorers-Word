using UnityEngine;
using UnityEditor;

namespace XplrUnityExtension
{
	/**
	 * Render Cubemap image from any point in scene
	 * taken from: https://docs.unity3d.com/ScriptReference/Camera.RenderToCubemap.html
	 */
	public class RenderCubemapWizard : ScriptableWizard {

		public Transform renderFromPosition;
		public Cubemap cubemap;

		void OnWizardUpdate () {
			helpString = "Select transform to render from and cubemap to render into";
			isValid = (renderFromPosition != null) && (cubemap != null);
		}
		/**
		 * Initialize new camera and save images to existing Cubemap
		 */
		void OnWizardCreate () {
			// create temporary camera for rendering
			GameObject go = new GameObject ("CubemapCamera");
			go.AddComponent<Camera>();
			// place it on the object
			go.transform.position = renderFromPosition.position;
			go.transform.rotation = Quaternion.identity;
			// render into cubemap
			go.GetComponent<Camera>().RenderToCubemap( cubemap );

			// destroy temporary camera
			DestroyImmediate( go );
		}
		/**
		 * The Menu entry
		 */
		[MenuItem("-µ-/Render into Cubemap")]
		static void RenderCubemap () {
			ScriptableWizard.DisplayWizard<RenderCubemapWizard>(
				"Render cubemap", "Render!");
		}
	}
}