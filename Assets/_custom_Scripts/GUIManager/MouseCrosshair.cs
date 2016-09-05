using UnityEngine;

namespace XplrGUI {
	/**
	 * Draw red dot instead of mouse cursor. Change image if interaction is possible
	 */
	public class MouseCrosshair : MonoBehaviour
	{
		public static bool showCrosshair = true;
		[SerializeField] private Texture2D textureDefault;
		[SerializeField] private Texture2D textureActive;
		[HideInInspector] public bool interactionPossible;

		/**
		* Display screen centered crosshair
		*/
		void OnGUI () {
			if (!showCrosshair)
				return;

			if (interactionPossible && textureActive != null)
			{
				Rect positionActive = new Rect(
					(Screen.width - textureActive.width) / 2,
					(Screen.height - textureActive.height) /2,
					textureActive.width, textureActive.height);
				GUI.DrawTexture (positionActive, textureActive);
			}
			else if (textureDefault != null)
			{
				Rect positionDefault = new Rect(
					(Screen.width - textureDefault.width) / 2,
					(Screen.height - textureDefault.height) /2,
					textureDefault.width, textureDefault.height);
				GUI.DrawTexture (positionDefault, textureDefault);
			}
		}
	}
}