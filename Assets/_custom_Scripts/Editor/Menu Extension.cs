using UnityEngine;
using UnityEditor;
using System.IO;

//public class MenuExtension {
//	[MenuItem("-µ-/Cleanup missing .meta")]
//	public static void ScriptedOps() {
//		
//	}
//}

public class CustomDebug : EditorWindow {
	
	private static Vector2 scrollPos;
	private static string filesToDelete = "";

	// Add menu named "My Window" to the Window menu
	[MenuItem ("-µ-/Custom Debug")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		CustomDebug window = (CustomDebug)EditorWindow.GetWindow (typeof (CustomDebug));
		window.Show();

		filesToDelete = "This tool will analyze the Assets folder and remove all .meta files " +
			"which do not correspond with an actual file";
	}

	void OnGUI () {
		GUILayout.Label (".meta Cleanup Tool", EditorStyles.boldLabel);
		
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Show only")) {
			filesToDelete = "Searching for .meta files...\n";
			clearMetaInFolder (false, "Assets");
			filesToDelete += "done.";
		}
		if (GUILayout.Button ("Clean")) {
			filesToDelete = "Searching for .meta files...\n";
			clearMetaInFolder (true, "Assets");
			filesToDelete += "Cleanup done.";
		}
		EditorGUILayout.EndHorizontal ();

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.HelpBox (filesToDelete, MessageType.None);
		EditorGUILayout.EndScrollView ();
	}


	private static void clearMetaInFolder(bool delete, string path) {

		var info = new DirectoryInfo (path);
		var fileInfo = info.GetFileSystemInfos ();


		foreach (FileSystemInfo item in fileInfo) {
			bool isDir = (item.Attributes == FileAttributes.Directory);
			bool isMeta = item.Extension.Equals (".meta");

			if (isDir) {
				clearMetaInFolder (delete, item.FullName);
				continue;

			} else if (isMeta) {

				bool flag = true;
				string nameWithoutMeta = item.Name.Substring (0, item.Name.Length - 5);

				foreach (FileSystemInfo cmp_item in fileInfo) {
					if (!cmp_item.Extension.Equals (".meta") && cmp_item.Name.Equals (nameWithoutMeta)) {
						flag = false;
					}
				}

				if (flag) {
					if (delete)
						item.Delete ();
					else
						filesToDelete += item.FullName + "\n";
				}
			}
		}
	}
}