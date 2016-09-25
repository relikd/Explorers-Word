using UnityEngine;
using UnityEditor;
using System.IO;

/** Contains all Unity Editor windows and developer helper tools */
namespace XplrUnityExtension
{
	/**
	 * Loop through all folders in /Assets/ and delete all .meta files without a counterpart
	 */
	public class DeleteObsoleteMetaFiles : EditorWindow
	{
		private static Vector2 scrollPos;
		private static string filesToDelete = "";

		/**
		 * Initialize new window with info text
		 */
		[MenuItem ("-µ-/Delete obsolete .meta files")]
		static void Init () {
			// Get existing open window or if none, make a new one:
			DeleteObsoleteMetaFiles window = (DeleteObsoleteMetaFiles)EditorWindow.GetWindow (typeof (DeleteObsoleteMetaFiles));
			window.Show();

			filesToDelete = "This tool will analyze the Assets folder and remove all .meta files " +
				"which do not correspond with an actual file";
		}
		/**
		 * Show options for cleaning tool
		 */
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
		/**
		 * Recursively go through all folders and either delete or just print file
		 */
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
}