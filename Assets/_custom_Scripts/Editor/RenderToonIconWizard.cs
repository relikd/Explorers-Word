using UnityEngine;
using UnityEditor;
using System;

namespace XplrUnityExtension
{
	/**
	 * Icon creation helper. Uses the current scene camera and renders multiple png images of selected object
	 */
	public class RenderToonIconWizard : ScriptableWizard {

		[Serializable]
		public enum IconShadingType {
			Toon,
			DirectionalLight,
			PointLight,
			BothLights,
			SaveAll
		}

		public IconShadingType shadingType = IconShadingType.SaveAll;
		public Transform renderObject;
		public string iconName;

		private bool setInitialObj = true;
		private bool setInitialName = true;
		private const int RESOLUTION = 512;
		private const string ICON_PATH = "Assets/_custom_Assets/Icons/";

		/**
		 * Wizard checking function. Used to activate the Render button and preset field values
		 */
		void OnWizardUpdate () {
			helpString = "Align your SceneView camera first.\nThats how the icon will be shown.";
			helpString += "\n( PATH: /" + ICON_PATH + " )";

			if (setInitialObj && renderObject == null && Selection.activeTransform) {
				renderObject = Selection.activeTransform;
				setInitialObj = false;
			}

			if (setInitialName && renderObject != null && (iconName == null || iconName.Length == 0)) {
				iconName = renderObject.name;
				setInitialName = false;
			}

			isValid = (renderObject != null) && (iconName != null && iconName.Length > 0);
		}
		/**
		 * Get active camera and render object with selected settings
		 */
		void OnWizardCreate () {
			Camera cam = SceneView.lastActiveSceneView.camera;

			CameraClearFlags prevClearFlag = cam.clearFlags;
			int prevCullingMask = cam.cullingMask;
			// set temporary mask for visibility
			cam.cullingMask = 1<<28;
			cam.clearFlags = CameraClearFlags.Depth;

			if (shadingType == IconShadingType.SaveAll) {
				shadingType = IconShadingType.Toon;
				copyObjectAndRender (cam, iconName+"_toon");
				shadingType = IconShadingType.DirectionalLight;
				copyObjectAndRender (cam, iconName+"_directional");
				shadingType = IconShadingType.PointLight;
				copyObjectAndRender (cam, iconName+"_point");
				shadingType = IconShadingType.BothLights;
				copyObjectAndRender (cam, iconName+"_both");
			} else {
				copyObjectAndRender (cam, iconName);
			}

			// restore prev settings
			cam.cullingMask = prevCullingMask;
			cam.clearFlags = prevClearFlag;
		}
		/**
		 * Create a copy of the object, attach all selected lights, take screenshot and delete copy
		 * @param camera which camera should be used to take screenshot
		 * @param fname filename for the screenshot
		 */
		void copyObjectAndRender (Camera camera, string fname) {
			GameObject aCopy = GameObject.Instantiate (renderObject.gameObject);
			SetLayerRecursively (aCopy, 28);

			if (shadingType == IconShadingType.DirectionalLight || shadingType == IconShadingType.BothLights)
				addDirectionalLight (aCopy.transform);
			if (shadingType == IconShadingType.PointLight || shadingType == IconShadingType.BothLights)
				addPointLight (aCopy.transform);

			TakeScreenshotWithCam (camera, fname);
			DestroyImmediate (aCopy);
		}
		/**
		 * Set the layer property of an object and all its children (destructive)
		 * @param obj should be a copy of the object
		 * @param newLayer int between [0 .. 31]
		 */
		void SetLayerRecursively(GameObject obj, int newLayer) {
			obj.layer = newLayer;
			foreach (Transform child in obj.transform)
				SetLayerRecursively (child.gameObject, newLayer);
		}
		/**
		 * Set camera render path to a local {@link RenderTexture} and convert that image file to png
		 * @param camera which camera should be used to take screenshot
		 * @param filename just the name, will be saved to a pre-defined location
		 */
		void TakeScreenshotWithCam(Camera camera, string filename) {
			RenderTexture prevTexture = camera.targetTexture;
			RenderTexture rt = new RenderTexture(RESOLUTION, RESOLUTION, 32, RenderTextureFormat.ARGB32);
			camera.targetTexture = rt;
			Texture2D screenShot = new Texture2D(RESOLUTION, RESOLUTION, TextureFormat.ARGB32, false);
			if (shadingType == IconShadingType.Toon)
				camera.RenderWithShader (Shader.Find ("Toon/Basic Outline"), null);
			else
				camera.Render ();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, RESOLUTION, RESOLUTION), 0, 0);
			camera.targetTexture = prevTexture;
			RenderTexture.active = null; // JC: added to avoid errors
			DestroyImmediate (rt);
			byte[] bytes = screenShot.EncodeToPNG();
			System.IO.File.WriteAllBytes(ICON_PATH + filename + ".png", bytes);
		}
		/**
		 * Create two directional light sources and attach them to the object
		 * @param parent the object where the light will be added
		 */
		void addDirectionalLight(Transform parent) {
			GameObject fst = new GameObject ();
			fst.AddComponent<Light> ();
			fst.GetComponent <Light>().type = LightType.Directional;
			fst.transform.eulerAngles = new Vector3 (145.3f, -30, 0);
			fst.transform.SetParent (parent);
			GameObject snd = new GameObject ();
			snd.AddComponent<Light> ();
			snd.GetComponent <Light>().type = LightType.Directional;
			snd.transform.eulerAngles = new Vector3 (-35, 30, -288);
			snd.transform.SetParent (parent);
		}
		/**
		 * Create a point light in the camera center and attach it to the object
		 * @param parent the object where the light will be added
		 */
		void addPointLight(Transform parent) {
			GameObject trd = new GameObject ();
			trd.AddComponent<Light> ();
			trd.GetComponent <Light>().type = LightType.Point;
			Vector3 newV = SceneView.lastActiveSceneView.camera.transform.position - trd.transform.position;
			trd.transform.position = newV;
			trd.transform.SetParent (parent);
		}
		/**
		 * Create a wizard window in our custom menu
		 */
		[MenuItem("-µ-/Render Toon Icon GUI", false, 1)]
		static void RenderToonIcon () {
			ScriptableWizard.DisplayWizard<RenderToonIconWizard>(
				"Render Toon Icon", "Render");
		}
		/**
		 * Same like {@link #RenderToonIcon()} but without a window prompt
		 */
		[MenuItem("-µ-/Render Toon Icon %t", false, 2)] // cmd + T (%=cmd/ctrl #=shift &=alt)
		static void RenderEffortlessWizardDetour() {
			RenderToonIconWizard rtiw = ScriptableWizard.CreateInstance<RenderToonIconWizard> ();
			rtiw.OnWizardUpdate ();
			if (rtiw.isValid)
				rtiw.OnWizardCreate ();
			DestroyImmediate (rtiw);
		}
		/**
		 * Checking method for wizardless version. User has to select an object in the hierarchy first
		 */
		[MenuItem("-µ-/Render Toon Icon %t", true)]
		static bool what() {
			return (Selection.activeTransform != null);
		}
	}
}