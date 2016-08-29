using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Collections.Generic;

/**
* Loads Next Level as well as a Story Chapter concerning the specific Level. 
*/
public class LevelManager : MonoBehaviour {

	static LevelManager Instance;
	public List<string> Chapter = new List<string>();
	//public string story;
	string Path = "";
	string CurrentStoryChapterName="StoryChapter0"; 

	/**
	* Instantiates Class Variables. 
	*/
	void Start () {
		this.Path = "" + Application.dataPath + "/StoryChapters/";
		int levelNr;
		
		if (shouldLoadParagraphs ()) {
			if (SceneManager.GetActiveScene ().name == "room_Tutorial") {
				Chapter = GetSplitParagraphs (LoadParagraphs (this.Path + "room_Tutorial" + ".txt"));
				Debug.Log (Chapter[Chapter.Count-1]);
			} else {
				Chapter = GetSplitParagraphs (LoadParagraphs (this.Path + this.CurrentStoryChapterName + getCurrentLevelNumber()+ ".txt"));
			}
		}
	}

	/**
	* Shortcuts For Loading Scenes. 
	*/
	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("room_1");
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("room_2");
		}
	}
		
	/**
	* Calls Method for Spliting a Paragraph. 
	*/
	private List<string> GetSplitParagraphs(string text) {
		List<string> result = new List<string> ();
		result.AddRange(createLinesForParagraph (text));
		return result;
	}

	/**
	* Splits a given text into Lines fitting a specific Threshhold. 
	*/
	private List<string> createLinesForParagraph(string text) {
		List<string> Lines = new List<string>();
		int LineThreshhold = 48;
		string oldLine = ""; 
		string currentLine = "";

		foreach (char c in text.ToCharArray()) {
			if (c == ' ') {
				if (currentLine.Length >= LineThreshhold) {
					Lines.Add (oldLine + "\n");
					currentLine = "";
					oldLine = "" + oldLine.ToCharArray()[(oldLine.Length-1)];
				}
			} 
			if (c  == '#') {
				Lines.Add (oldLine);
			}
			currentLine += c;
			oldLine = currentLine;
		}
		return Lines;
	}

	/**
	* Loads the next Room. 
	*/
	static public void LoadNextRoom() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		levelNumber++;
		SceneManager.LoadScene ("room_"+levelNumber, LoadSceneMode.Single);
	}

	static public void LoadStartScreen() {
		SceneManager.LoadScene ("startMenu",LoadSceneMode.Single);
	}

	static public void LoadRoom (string roomName){
		SceneManager.LoadScene (roomName, LoadSceneMode.Single);
	}

	/**
	* Returns the current Chapter. 
	*/
	public List<string> getChapter() {
		return Chapter;
	}

	private bool shouldLoadParagraphs() {
		string[] x = { "room_Tutorial", "room_1", "room_2", "room_3", "room_4" };
		bool result = false;
		string levelName = SceneManager.GetActiveScene ().name;
		foreach (string str in x) {
			if (str.ToUpper() == levelName.ToUpper()) {
				result = true;
			}
		}
		return result;
	}

	/**
	* Returns the current Level Number. 
	*/
	private int getCurrentLevelNumber() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		return levelNumber;
	}

	/**
	* Loads the a specific Chapter Textfile given the Name. 
	*/
	private string LoadParagraphs(string fileName)
	{
		return File.ReadAllText (fileName);
	}


}