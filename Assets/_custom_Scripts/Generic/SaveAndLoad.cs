using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
/// <summary>
/// Saves and Loads the game states
/// </summary>
public class SaveAndLoad : MonoBehaviour {

	private static int _currentHighestEnabledLevel;
	/// <summary>
	/// Gets or sets the levels completed. Saves and Loads internal.
	/// </summary>
	/// <value>The levels completed.</value>
	public static int LevelsCompleted{
		get{
			_currentHighestEnabledLevel = LoadLevelsCompleted();
			return _currentHighestEnabledLevel;
		}
		set{ 
			_currentHighestEnabledLevel = value;
			SaveLevelsCompleted ();
		}
	}

	/// <summary>
	/// Datapath to the file containing the information about the completed levels
	/// </summary>
	private static string levelsCompletedPath = Application.persistentDataPath + "/levelsCompleted.data";

	/// <summary>
	/// Saves the levels completed.
	/// </summary>
	/// <param name="newSaveState">New save state.</param>
	private static void SaveLevelsCompleted()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (levelsCompletedPath, FileMode.OpenOrCreate);

		bf.Serialize (file, _currentHighestEnabledLevel.ToString());
		file.Close ();
	}

	/// <summary>
	/// Loads the levels completed.
	/// </summary>
	/// <returns>The levels completed.</returns>
	private static int LoadLevelsCompleted(){
		if (File.Exists (levelsCompletedPath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (levelsCompletedPath, FileMode.Open);
			string numberAsString = (string)bf.Deserialize (file);
			int result = 0;
			int.TryParse (numberAsString, out result);
			file.Close ();

			return result;
		} else {
			return 0;
		}
	}
}
