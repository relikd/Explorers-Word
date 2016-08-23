using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	static LevelManager Instance;
	string text = "";
	public List<string> Paragraphs = new List<string>();
	string Path = "/Users/mr.nignag/projektarbeit-explorers-word/Andere Files/StoryChapters/";
	string CurrentStoryChapterName="StoryChapter0"; 


	void Start () {
//		if (Instance != null)
//			GameObject.Destroy (gameObject);
//		else {
//			GameObject.DontDestroyOnLoad (gameObject);
//			Instance = this;
//		}
		this.LoadParagraphs (this.Path + this.CurrentStoryChapterName + getCurrentLevelNumber()+ ".txt");
		Paragraphs = GetSplitParagraphs (text);
		Debug.Log (Paragraphs.Count);
		Debug.Log (Paragraphs[60]);
	}

	private List<string> GetSplitParagraphs(string text) {
		List<string> result = new List<string> ();
		result.AddRange(createLinesForParagraph (text));
		return result;
	}

	private List<string> createLinesForParagraph(string text) {
		List<string> Lines = new List<string>();
		int LineThreshhold = 20;
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

	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("room_1");
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("room_2");
		}
	}

	static public void LoadNextRoom() {
//		MonoBehaviour[] allScripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
//		for (int i = allScripts.Length; i > 0; i--) {
//			if (allScripts[i-1] != Instance) {
//				Destroy (allScripts[i-1]);
//			}
//		}
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		levelNumber++;

		SceneManager.LoadScene ("room_"+levelNumber, LoadSceneMode.Single);
	}

	public List<string> getParagraphs() {
		return Paragraphs;
	}

	private int getCurrentLevelNumber() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		return levelNumber;
	}

	private bool LoadParagraphs(string fileName)
	{
		try
		{
			string paragraph = "";
			string line = "";
			StreamReader theReader = new StreamReader(fileName, Encoding.UTF8);

			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					Debug.Log(line);
					text += line;
				
				}
				while (line != null);
				theReader.Close();
				return true;
			}
		}

		catch (IOException e)
		{
			Debug.Log(e.Message);
			return false;
		}
	}
}
	
