using UnityEngine;

/**
* Fading for Scene Start or End
*/
public class SceneFadingScript : MonoBehaviour
{
	public float fadeSpeed = 0.4f;

	private float progress = 0;
	private bool fadeIn = true;

	/** Do a fade in for every scene */
	void Awake() {
		BeginFadeIn ();
	}
	/** Draw white texture over the whole screen, gradually change alpha value */
	void OnGUI () {
		progress += Time.deltaTime;

		float alpha = Mathf.Clamp01 (progress / fadeSpeed);
		if (fadeIn)
			alpha = 1 - alpha;
		
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = -1000;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
	}
	/** Start fade in and return time */
	public float BeginFadeIn () {
		progress = 0;
		fadeIn = true;
		return (fadeSpeed);
	}
	/** Start fade out and return time */
	public float BeginFadeOut () {
		progress = 0;
		fadeIn = false;
		return (fadeSpeed);
	}
}
