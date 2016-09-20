using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ScreenshotCreator))]
public class ScreenshotCreatorEditor : Editor {
	[SerializeField] ScreenshotCreator script;

	void OnEnable(){
		script = (ScreenshotCreator)target;
	}

	// reset all X questions to standard
	void OnDisable(){
		refreshRequests ();
	}
	
	void refreshRequests(){
		for (int i = 0; i < script.list.Count; i++) {
			script.list[i].deleteQuestion = false;
		}
	}

	public override void OnInspectorGUI() {
		EditorUtility.SetDirty (target);

		script.superSize = EditorGUILayout.IntSlider("size factor", script.superSize, 1, 16);
		EditorGUILayout.LabelField(Screen.width * script.superSize + "x" + Screen.height * script.superSize);

		EditorGUILayout.Space ();

		script.screenshotName = EditorGUILayout.TextField("name", script.screenshotName);
		EditorGUILayout.SelectableLabel("save path = " + System.IO.Directory.GetCurrentDirectory() + "/Screenshots/");

		for (int i = 0; i < script.list.Count; i++) {
			ScreenshotCreator.CameraObject c = script.list[i];

			EditorGUILayout.BeginHorizontal ();

			GUI.color = Color.white;
			script.list[i].cam = (GameObject) EditorGUILayout.ObjectField(script.list[i].cam, typeof(GameObject), true);

			EditorGUI.BeginDisabledGroup (!EditorApplication.isPlaying);
			if (script.list [i].cam != null) {
				if (GUILayout.Button ("USE " + script.list [i].cam.name, new GUIStyle(EditorStyles.miniButtonLeft))) {
					refreshRequests();
					script.CaptureScreenshots (i);
				}
			}
			EditorGUI.EndDisabledGroup();

			// the delete button
			if (c.deleteQuestion){
				GUI.color = Color.red;
				if (GUILayout.Button ("YES?", new GUIStyle(EditorStyles.miniButtonRight), GUILayout.MaxWidth(45), GUILayout.MaxHeight(14))) {
					refreshRequests();
					script.Delete (i);
				}
			} else {
				GUI.color = (Color.red + Color.white) / 2f;
				if (GUILayout.Button ("X", new GUIStyle(EditorStyles.miniButtonRight), GUILayout.MaxWidth(45), GUILayout.MaxHeight(14))) {
					refreshRequests();
					script.RequestDelete (i);
				}
			}

			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
		}

		GUI.color = new Color (0.54f, 0.68f, 0.95f);
		if(GUILayout.Button("ADD CAMERA", GUILayout.MaxWidth (100), GUILayout.MinWidth (100), GUILayout.MaxHeight (25), GUILayout.MinHeight (25))) {
			refreshRequests();
			script.Create ();
		}
	}
}
#endif