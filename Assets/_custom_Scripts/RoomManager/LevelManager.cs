using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO; 
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	static LevelManager Instance;
	List<string> Paragraphs = new List<string>();
	string Path = "/Users/mr.nignag/projektarbeit-explorers-word/Andere Files/StoryChapters/";
	string CurrentStoryChapterName="StoryChapter0"; 


	void Start () {
//		if (Instance != null)
//			GameObject.Destroy (gameObject);
//		else {
//			GameObject.DontDestroyOnLoad (gameObject);
//			Instance = this;
//		}
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			SceneManager.LoadScene ("room_1");
			this.LoadParagraphs (this.Path + this.CurrentStoryChapterName + getCurrentLevelNumber()+ ".txt");
			printParagraphs ();
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			SceneManager.LoadScene ("room_2");
//			this.LoadParagraphs (this.Path + this.CurrentStoryChapterName + getCurrentLevelNumber() + ".txt");
//			printParagraphs ();
		}
	}

	void printParagraphs() {
		foreach (string str in Paragraphs) {
			Debug.Log (str + "\n" + "\n");
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

	private int getCurrentLevelNumber() {
		string levelName = SceneManager.GetActiveScene ().name.Substring (5);
		int levelNumber = 0;
		int.TryParse (levelName, out levelNumber);
		Debug.Log ("LVL" + levelNumber);
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

					if (line == " ")
					{
						Paragraphs.Add(paragraph);
						paragraph = "";

					} else {
						paragraph += line;
					}

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
	
