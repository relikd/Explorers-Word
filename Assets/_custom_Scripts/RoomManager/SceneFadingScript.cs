using UnityEngine;
using System.Collections;

public class SceneFadingScript : MonoBehaviour {

	public Texture2D fadeTexture;
	public float fadeSpeed = 0.4f;

	private int drawDepth;
	private float alpha = 1.0f;
	private int fadeDir = -1;

	void OnGUI(){
		fadeTexture = Texture2D.whiteTexture;
		alpha += fadeDir * fadeSpeed * Time.deltaTime;

		alpha = Mathf.Clamp01 (alpha);

		drawDepth = -1000;
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
	}


	public float BeginFade(int direction){
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnSceneWasLoaded(){
		BeginFade (-1);
	}
}
