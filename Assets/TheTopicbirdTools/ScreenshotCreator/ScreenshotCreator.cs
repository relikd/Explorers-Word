using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode] public class ScreenshotCreator : MonoBehaviour {
	[System.Serializable] public class CameraObject {
		public GameObject cam;
		public bool deleteQuestion = false;
	}

	[Tooltip("Select the screenshot resolution multiplier. If you select 1, the screenshot taken will have the same resolution as your Game View.")]
	[Range(1, 16)] public int superSize = 2;

	[Tooltip("The name of your screenshot or screenshot session. Camera name and current date will be added automatically.")]
	public string screenshotName = "";

	[Tooltip("Select the number of cameras and drag them in here. If you want to use multiple cameras at the same time (e. g. with different depth layers), insert their parent Gameobject.")]
	public List <CameraObject> list = new List<CameraObject>();

	public void CaptureScreenshots(int id){
		for (int i = 0; i < list.Count; i++) {
			if (list[i].cam != null)
			list [i].cam.SetActive (false);
		}
		list[id].cam.SetActive (true);

		if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Screenshots/")){
			var folder = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Screenshots/");
		}

		string s = Directory.GetCurrentDirectory() + "/Screenshots/";

		if (screenshotName != "") {
			s += screenshotName + "_";
		}
		s += list[id].cam.name + "_";
		s += System.DateTime.Now.Year + "_"; 
		s += System.DateTime.Now.Month + "_"; 
		s += System.DateTime.Now.Day + "_"; 
		s += System.DateTime.Now.Hour + "_"; 
		s += System.DateTime.Now.Minute + "_";
		s += System.DateTime.Now.Second;
		s += ".png";
		Debug.Log ("New screenshot: " + s);

		Application.CaptureScreenshot (s, superSize);
	}

	public void Create(){
		list.Add (new CameraObject());
	}

	public void RequestDelete (int id){
		list [id].deleteQuestion = true;
	}

	public void Delete (int id){
		list.Remove (list [id]);
	}
}